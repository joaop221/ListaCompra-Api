using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using ListaCompra.Infraestrutura.Tratamento;
using ListaCompra.Modelo.API.Lista;
using ListaCompra.Modelo.API.Produto;
using ListaCompra.Modelo.Base;
using ListaCompra.Modelo.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using API = ListaCompra.Modelo.API;

namespace ListaCompra.Negocio
{
    public class NegocioLista : INegocio
    {
        private readonly IRepositorio<Lista> repositorio;
        private readonly IRepositorio<GrupoUsuario> repositorioGrupoUsuario;
        private readonly IRepositorio<ProdutoLista> repositorioProdutoLista;
        private readonly UserManager<IdentityUser> userManager;
        private readonly HttpContext httpContext;
        private readonly NegocioGrupo negocioGrupo;
        private readonly IMapper mapper;

        public NegocioLista(IRepositorio<Lista> repositorio,
                            IMapper mapper,
                            UserManager<IdentityUser> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IRepositorio<GrupoUsuario> repositorioGrupoUsuario,
                            IRepositorio<ProdutoLista> repositorioProdutoLista,
                            IRepositorio<Produto> repositorioProduto,
                            IRepositorio<Categoria> repositorioCategoria,
                            NegocioGrupo negocioGrupo,
                            NegocioCategoria negocioCategoria,
                            NegocioProduto negocioProduto)
        {
            this.repositorio = repositorio;
            this.userManager = userManager;
            this.httpContext = httpContextAccessor.HttpContext;
            this.repositorioGrupoUsuario = repositorioGrupoUsuario;
            this.repositorioProdutoLista = repositorioProdutoLista;
            this.negocioGrupo = negocioGrupo;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter dados da Lista
        /// </summary>
        public async Task<ListaComProduto> Obter(int id)
        {
            // tasks
            Task<Lista> entidadeTask = this.repositorio.ObterAsync(x => x.Id == id, new ListaEntidade<Lista>(x => x.Grupo));
            Lista entidade = await entidadeTask;

            if (entidade == null)
                throw new ApiExcecao(422, "Lista não existe");

            if (await ValidaParticipante(entidade.Id) == false)
                throw new ApiExcecao(403, "Usuario não pode ver a lista pois não é membro do grupo dessa lista");

            List<ProdutoLista> listaProduto = await this.repositorioProdutoLista.ConsultarAsync(x => x.ListaId == id,
                                                                                                new ListaEntidade<ProdutoLista>(y => y.Produto));


            ListaComProduto response = this.mapper.Map<ListaComProduto>(entidade);
            response.Produtos = this.mapper.Map<List<ProdutoResponse>>(listaProduto.Select(x => x.Produto).ToList());

            return response;
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ListaResponse>> Listar()
        {
            List<Lista> entidade = await this.repositorio.ConsultarAsync(x => x.Excluido == false);

            return this.mapper.Map<List<ListaResponse>>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ListaResponse>> ListarMinhasListas()
        {
            // TODO: criar repositorio com método especifico para isso
            // para n precisar trazer todas as listas
            List<Lista> listas = await this.repositorio.ConsultarAsync(x => x.Excluido == false);

            listas = listas.Where(x => ValidaParticipante(x.Id).Result).ToList();

            return this.mapper.Map<List<ListaResponse>>(listas);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ListaResponse> Criar(ListaRequest model)
        {
            Lista entidade = this.mapper.Map<Lista>(model);

            entidade = await this.repositorio.InserirAsync(entidade);

            return this.mapper.Map<ListaResponse>(entidade);
        }

        /// <summary>
        /// Criar lista com grupo
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ListaResponse> CriarComGrupo(ListaComGrupoRequest model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var entidade = new Lista()
                {
                    Titulo = model.Titulo
                };

                API.Grupo.GrupoResponse novoGrupo = await this.negocioGrupo.Criar(model.Grupo);
                entidade.GrupoId = novoGrupo.Id;

                entidade = await this.repositorio.InserirAsync(entidade);
                entidade.Grupo = this.mapper.Map<Grupo>(novoGrupo);

                scope.Complete();
                return this.mapper.Map<ListaResponse>(entidade);
            }
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task Atualizar(int id, ListaRequest model)
        {
            Lista entidade = this.mapper.Map<Lista>(model);
            entidade.Id = id;

            var isDono = await ValidaPermissaoDonoAsync(id);
            var isModerador = await ValidaPermissaoModeradorAsync(id);

            if (!isDono || !isModerador)
                throw new ApiExcecao(403, "Usuario não pode atualizar a lista, pois não é dono ou moderador");

            await this.repositorio.AtualizarAsync(entidade);
        }

        /// <summary>
        /// Excluir
        /// </summary>
        public async Task Excluir(int id)
        {
            var isDono = await ValidaPermissaoDonoAsync(id);
            var isModerador = await ValidaPermissaoModeradorAsync(id);

            if (!isDono || !isModerador)
                throw new ApiExcecao(403, "Usuario não pode atualizar a lista, pois não é dono ou moderador");

            await this.repositorio.ExcluirAsync(x => x.Id == id);
        }

        #region [ Métodos privados ]

        /// <summary>
        /// Valida se o usuario atual faz parte da Lista
        /// </summary>
        /// <param name="listaId"></param>
        /// <returns></returns>
        private async Task<bool> ValidaParticipante(int listaId)
        {
            // Tasks
            Task<IdentityUser> usuarioLogadoTask = RecuperaUsuarioAtualAsync();
            Task<Lista> entidadeTask = this.repositorio.ObterAsync(x => x.Id == listaId && x.Excluido == false,
                                                                    new ListaEntidade<Lista>(x => x.Grupo));

            // Await
            IdentityUser usuarioLogado = await usuarioLogadoTask;
            Lista entidade = await entidadeTask;

            // Recupera usuario e permissoes do grupo
            List<GrupoUsuario> permissoesGrupo = await this.repositorioGrupoUsuario.ConsultarAsync(x => x.GrupoId == entidade.GrupoId
                                                                                                        && x.Excluido == false);
            return permissoesGrupo.Any(x => x.UsuarioId == usuarioLogado.Id);
        }

        /// <summary>
        /// Recupera a permissao do usuario atual na lista
        /// </summary>
        /// <param name="listaId"></param>
        /// <returns></returns>
        private async Task<GrupoUsuario> RecuperaPermissao(int listaId)
        {
            // Tasks
            Task<IdentityUser> usuarioLogadoTask = RecuperaUsuarioAtualAsync();
            Task<Lista> entidadeTask = this.repositorio.ObterAsync(x => x.Id == listaId && x.Excluido == false,
                                                                    new ListaEntidade<Lista>(x => x.Grupo));

            // Await
            IdentityUser usuarioLogado = await usuarioLogadoTask;
            Lista entidade = await entidadeTask;

            // Recupera usuario e permissoes do grupo
            GrupoUsuario permissoeGrupo = await this.repositorioGrupoUsuario.ObterAsync(x => x.GrupoId == entidade.GrupoId
                                                                                            && x.UsuarioId == usuarioLogado.Id
                                                                                            && x.Excluido != false);
            if (permissoeGrupo == null)
                throw new ApiExcecao(403, "Usuario não pode ver a lista pois não é membro da lista");

            return permissoeGrupo;
        }

        /// <summary>
        /// Verifica se o usuario é Membro da lista
        /// </summary>
        /// <param name="usuario"></param>
        private async Task<bool> ValidaPermissaoDonoAsync(int listaId)
        {
            // Recupera permissao do usuario atual
            GrupoUsuario permissaoUsuario = await RecuperaPermissao(listaId);

            return permissaoUsuario.PermissaoId == (int)API.Permissao.Dono;
        }

        /// <summary>
        /// Verifica se o usuario é Membro da lista
        /// </summary>
        /// <param name="usuario"></param>
        private async Task<bool> ValidaPermissaoModeradorAsync(int listaId)
        {
            // Recupera permissao do usuario atual
            GrupoUsuario permissaoUsuario = await RecuperaPermissao(listaId);

            return permissaoUsuario.PermissaoId == (int)API.Permissao.Moderador;
        }

        /// <summary>
        /// Recupera usuario logado
        /// </summary>
        /// <returns></returns>
        private async Task<IdentityUser> RecuperaUsuarioAtualAsync()
        {
            Task<IdentityUser> usuarioAtualTask = this.userManager.GetUserAsync(this.httpContext.User); // recupera usuario atual
            IdentityUser usuarioAtual = await usuarioAtualTask;
            return usuarioAtual;
        }

        #endregion [ Métodos privados ]
    }
}