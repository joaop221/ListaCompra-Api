using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using ListaCompra.Infraestrutura.Tratamento;
using ListaCompra.Modelo.API.Produto;
using ListaCompra.Modelo.Base;
using ListaCompra.Modelo.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ListaCompra.Negocio
{
    public class NegocioProduto : INegocio
    {
        private readonly IRepositorio<Produto> repositorio;
        private readonly IRepositorio<ProdutoLista> repositorioProdutoLista;
        private readonly IRepositorio<GrupoUsuario> repositorioGrupoUsuario;
        private readonly IRepositorio<Categoria> repositorioCategoria;
        private readonly IRepositorio<Lista> repositorioLista;
        private readonly UserManager<IdentityUser> userManager;
        private readonly HttpContext httpContext;
        private readonly NegocioGrupo negocioGrupo;
        private readonly NegocioCategoria negocioCategoria;
        private readonly IMapper mapper;


        public NegocioProduto(IRepositorio<Produto> repositorio,
                            IMapper mapper,
                            UserManager<IdentityUser> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IRepositorio<GrupoUsuario> repositorioGrupoUsuario,
                            IRepositorio<ProdutoLista> repositorioProdutoLista,
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
            this.repositorioCategoria = repositorioCategoria;
            this.negocioGrupo = negocioGrupo;
            this.negocioCategoria = negocioCategoria;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter
        /// </summary>
        public async Task<ProdutoResponse> Obter(int id)
        {
            Produto entidade = await this.repositorio.ObterAsync(id);

            return this.mapper.Map<ProdutoResponse>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ProdutoResponse>> Listar()
        {
            List<Produto> entidade = await this.repositorio.ConsultarAsync(x => x.Excluido == false);

            return this.mapper.Map<List<ProdutoResponse>>(entidade);
        }

        /// <summary>
        /// Listar
        /// </summary>
        public async Task<List<ProdutoResponse>> Listar(string termo)
        {
            List<Produto> entidade = await this.repositorio.ConsultarAsync(x => x.Nome.Contains(termo) && x.Excluido == false);

            return this.mapper.Map<List<ProdutoResponse>>(entidade);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task<ProdutoResponse> Criar(ProdutoRequest model)
        {
            Produto entidade = this.mapper.Map<Produto>(model);

            entidade = await this.repositorio.InserirAsync(entidade);

            return this.mapper.Map<ProdutoResponse>(entidade);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task CriarNaLista(ProdutoRequest model, int listaId)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                // Verifica se o usuario tem permissao na lista
                var permissao = await ValidaParticipante(listaId);
                if (permissao == false)
                    throw new ApiExcecao(403, "Usuario não pode adicionar produto na lista pois não participa desta lista");

                // Cria o produto
                Produto entidade = await GaranteProduto(model);

                var produtoLista = new ProdutoLista()
                {
                    ListaId = listaId,
                    ProdutoId = entidade.Id
                };

                // Atrela a lista com o produto
                await this.repositorioProdutoLista.InserirAsync(produtoLista);

                scope.Complete();
            }
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task CriarNaLista(CriaProdutoComListaRequest produtoComLista)
            => await CriarNaLista(produtoComLista.Produto, produtoComLista.ListaId);

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        public async Task CriarNaLista(CriaListaProdutoComListaRequest listaProdutoComLista)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (ProdutoRequest produto in listaProdutoComLista.Produtos)
                {
                    await CriarNaLista(produto, listaProdutoComLista.ListaId);
                }

                scope.Complete();
            }
        }

        /// <summary>
        /// Atualizar
        /// </summary>
        public async Task Atualizar(int id, ProdutoRequest model)
        {
            Produto entidade = this.mapper.Map<Produto>(model);
            entidade.Id = id;

            await this.repositorio.AtualizarAsync(entidade);
        }

        /// <summary>
        /// Excluir
        /// </summary>
        public async Task Excluir(int id) => await this.repositorio.ExcluirAsync(x => x.Id == id);

        #region [ Métodos privados ]

        private async Task<Produto> GaranteProduto(ProdutoRequest model)
        {
            Produto entidade = null;
            // Novo produto
            if (model.Id == null)
            {
                Categoria categoria = await GaranteCategoria(model);

                model.CategoriaId = categoria.Id;
                entidade = this.mapper.Map<Produto>(await this.negocioProduto.Criar(model));
            }
            else
            {
                // Verifica se o produto existe
                entidade = await this.ObterAsync(model.Id);
                if (entidade == null)
                    throw new ApiExcecao(422, "Este produto não existe");

                entidade = this.mapper.Map<Produto>(model);
            }

            return entidade;
        }

        private async Task<Categoria> GaranteCategoria(ProdutoRequest model)
        {
            Categoria categoria = null;
            // Nova categoria
            if (model.CategoriaId == null)
                categoria = this.mapper.Map<Categoria>(await this.negocioCategoria.Criar(model.Categoria));
            else
            {
                // Verifica se o produto existe
                categoria = await this.repositorioCategoria.ObterAsync(model.CategoriaId);
                if (categoria == null)
                    throw new ApiExcecao(422, "Esta categoria não existe");

                categoria = this.mapper.Map<Categoria>(model.Categoria);
            }

            return categoria;
        }

        /// <summary>
        /// Valida se o usuario atual faz parte da Lista
        /// </summary>
        /// <param name="listaId"></param>
        /// <returns></returns>
        private async Task<bool> ValidaParticipante(int listaId)
        {
            // Tasks
            Task<IdentityUser> usuarioLogadoTask = RecuperaUsuarioAtualAsync();
            Task<Lista> entidadeTask = this.repositorioLista.ObterAsync(x => x.Id == listaId && x.Excluido == false,
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