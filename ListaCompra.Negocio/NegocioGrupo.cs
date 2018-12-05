using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using ListaCompra.Infraestrutura.Tratamento;
using ListaCompra.Infraestrutura.Util;
using ListaCompra.Modelo.API.Conta;
using ListaCompra.Modelo.API.Grupo;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using API = ListaCompra.Modelo.API;

namespace ListaCompra.Negocio
{
    public class NegocioGrupo : INegocio
    {
        private readonly IRepositorioGrupo repositorio;
        private readonly IRepositorio<GrupoUsuario> repositorioGrupoUsuario;
        private readonly IMapper mapper;
        private readonly UserManager<IdentityUser> userManager;
        private readonly HttpContext httpContext;


        public NegocioGrupo(IRepositorioGrupo repositorio,
                            UserManager<IdentityUser> userManager,
                            IHttpContextAccessor httpContextAccessor,
                            IRepositorio<GrupoUsuario> repositorioGrupoUsuario,
                            IMapper mapper)
        {
            this.repositorio = repositorio;
            this.userManager = userManager;
            this.httpContext = httpContextAccessor.HttpContext;
            this.repositorioGrupoUsuario = repositorioGrupoUsuario;
            this.mapper = mapper;
        }

        /// <summary>
        /// Obter
        /// </summary>
        public async Task<GrupoComUsuariosResponse> Obter(int id)
        {
            // Consultas assincronas
            Task<Grupo> entidadeTask = this.repositorio.ObterAsync(id);
            Task<List<IdentityUser>> usuariosTask = this.repositorio.ConsultaUsuariosDoGrupoAsync(id);

            Grupo entidade = await entidadeTask;
            List<IdentityUser> usuarios = await usuariosTask;

            GrupoResponse grupoResponse = this.mapper.Map<GrupoResponse>(entidade);
            List<Usuario> listaUsuarios = this.mapper.Map<List<Usuario>>(usuarios);

            return new GrupoComUsuariosResponse() { Grupo = grupoResponse, Usuarios = listaUsuarios };
        }

        /// <summary>
        /// Lista de grupo
        /// </summary>
        public async Task<List<GrupoResponse>> Listar()
        {
            List<Grupo> entidade = await this.repositorio.ConsultarAsync(x => x.Excluido == false);

            return this.mapper.Map<List<GrupoResponse>>(entidade);
        }

        /// <summary>
        /// Grupos que o usuario atual faz parte
        /// </summary>
        /// <returns></returns>
        public async Task<List<GrupoResponse>> ListarGrupoUsuarioAtual()
        {
            // Cria um novo grupo e recupera o usuario atual
            IdentityUser usuarioAtual = await RecuperaUsuarioAtualAsync();

            List<GrupoUsuario> gruposDoUsuario = await this.repositorioGrupoUsuario.ConsultarAsync(x => x.UsuarioId == usuarioAtual.Id
                                                                                                && x.Excluido == false);

            List<Grupo> entidade = await this.repositorio.ConsultarAsync(x => gruposDoUsuario.Any(y => y.GrupoId == x.Id)
                                                                                    && x.Excluido == false);

            return this.mapper.Map<List<GrupoResponse>>(entidade);
        }

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Grupo criado</returns>
        public async Task<GrupoResponse> Criar(GrupoRequest model)
        {
            Grupo entidade = this.mapper.Map<Grupo>(model);

            // Cria um novo grupo e recupera o usuario atual
            Task<IdentityUser> usuarioTask = RecuperaUsuarioAtualAsync();
            Task<Grupo> entidadeTask = this.repositorio.InserirAsync(entidade);
            IdentityUser usuario = await usuarioTask;
            entidade = await entidadeTask;

            // Adiciona o usuario no proprio grupo que está sendo criando
            await this.repositorioGrupoUsuario.InserirAsync(new GrupoUsuario(entidade.Id, usuario.Id, 1));

            return this.mapper.Map<GrupoResponse>(entidade);
        }

        /// <summary>
        /// Criar grupo com usuarios
        /// </summary>
        /// <returns>Grupo criado</returns>
        public async Task<GrupoResponse> CriarComUsuarios(GrupoComUsuariosRequest model)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                Grupo entidadeGrupo = this.mapper.Map<Grupo>(model.Grupo);
                List<IdentityUser> entidadeUsuarios = this.mapper.Map<List<IdentityUser>>(model.Usuarios);

                // Cria um novo grupo e recupera o usuario atual
                Task<IdentityUser> usuarioTask = RecuperaUsuarioAtualAsync();
                Task<Grupo> entidadeTask = this.repositorio.InserirAsync(entidadeGrupo);
                IdentityUser usuario = await usuarioTask;
                entidadeGrupo = await entidadeTask;

                entidadeUsuarios = RecuperaUsuarios(entidadeUsuarios, usuario);

                foreach (IdentityUser item in entidadeUsuarios)
                {
                    GrupoUsuario relacionamento;
                    if (item.UserName.ToUpper() == usuario.UserName.ToUpper())
                        relacionamento = new GrupoUsuario(entidadeGrupo.Id, item.Id, 1); // usuario Dono
                    else
                        relacionamento = new GrupoUsuario(entidadeGrupo.Id, item.Id, 3); // Usuario contribuidor 

                    // Adiciona o usuario no proprio grupo que está sendo criando
                    await this.repositorioGrupoUsuario.InserirAsync(relacionamento);
                }

                scope.Complete();
                return this.mapper.Map<GrupoResponse>(entidadeGrupo);
            }
        }

        /// <summary>
        /// Adiciona usuario em um grupo
        /// </summary>
        public async Task AdicionarUsuario(int grupoId, Usuario usuario)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await ValidaUsuarioPermissaoAsync(grupoId, usuario);

                IdentityUser entidadeUsuario = await RecuperaUsuarioPeloNome(usuario.Nome);

                // Cria um novo grupo e recupera o usuario atual
                Task<Grupo> grupoTask = this.repositorio.ObterAsync(grupoId); // recupera grupo pelo ID
                Grupo grupo = await grupoTask;

                var relacionamento = new GrupoUsuario()
                {
                    GrupoId = grupo.Id,
                    UsuarioId = entidadeUsuario.Id,
                    PermissaoId = usuario.Permissao == API.Grupo.Permissao.Padrao ? 3 : (int)usuario.Permissao
                };
                await this.repositorioGrupoUsuario.InserirAsync(relacionamento);

                scope.Complete();
            }
        }

        /// <summary>
        /// Adiciona usuarios em um grupo
        /// </summary>
        public async Task AdicionarUsuario(int grupoId, List<Usuario> usuarios)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                foreach (Usuario usuario in usuarios)
                    await AdicionarUsuario(grupoId, usuario);

                scope.Complete();
            }
        }

        /// <summary>
        /// Altera permissao do usuario em um grupo
        /// </summary>
        public async Task AlterarPermissao(int grupoId, Usuario usuario)
        {
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await ValidaUsuarioPermissaoAsync(grupoId, usuario);

                // Recupera usuario pelo Nome
                IdentityUser entidadeUsuario = await RecuperaUsuarioPeloNome(usuario.Nome);

                // Altera a permissao do usuario no Grupo
                GrupoUsuario relacionamento = await this.repositorioGrupoUsuario.ObterAsync(x => x.UsuarioId == entidadeUsuario.Id &&
                                                                                            x.GrupoId == grupoId &&
                                                                                            x.Excluido == false);

                if (relacionamento == null)
                    throw new ApiExcecao(422, "Usuario não pertence ao grupo");

                relacionamento.PermissaoId = (int)usuario.Permissao;

                // Atualiza a permissao do usuario
                await this.repositorioGrupoUsuario.AtualizarAsync(relacionamento);

                // Commita a transacao
                scope.Complete();
            }
        }

        /// <summary>
        /// Atualiza grupo
        /// </summary>
        public async Task Atualizar(int id, GrupoRequest model)
        {
            Grupo entidade = this.mapper.Map<Grupo>(model);
            entidade.Id = id;

            IdentityUser usuarioAtual = await RecuperaUsuarioAtualAsync();

            // Verifica permissao do usuario atual
            GrupoUsuario grupoUsuario = await this.repositorioGrupoUsuario.ObterAsync(x => x.UsuarioId == usuarioAtual.Id &&
                                                                                            x.GrupoId == id &&
                                                                                            x.Excluido == false);

            if (grupoUsuario == null)
                throw new ApiExcecao(403, "Usuario não pode excluir permissao pois não é membro do grupo");
            if (grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Dono
                && grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Moderador) // Apenas o dono ou moderador pode alterar o grupo
                throw new ApiExcecao(403, "Usuario não pode alterar o grupo pois não é dono ou moderador do grupo");

            await this.repositorio.AtualizarAsync(entidade);
        }

        /// <summary>
        /// Excluir grupo
        /// </summary>
        public async Task Excluir(int id)
        {
            IdentityUser usuarioAtual = await RecuperaUsuarioAtualAsync();

            // Verifica permissao do usuario atual
            GrupoUsuario grupoUsuario = await this.repositorioGrupoUsuario.ObterAsync(x => x.UsuarioId == usuarioAtual.Id &&
                                                                                            x.GrupoId == id &&
                                                                                            x.Excluido == false);

            if (grupoUsuario == null)            
                throw new ApiExcecao(403, "Usuario não pode excluir permissao pois não é membro do grupo");            
            if (grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Dono) // Apenas o dono pode excluir o grupo            
                throw new ApiExcecao(403, "Usuario não pode excluir dono pois não é dono do grupo");
            
            await this.repositorio.ExcluirAsync(x => x.Id == id);
        }

        #region [ Métodos privados ]

        /// <summary>
        /// Recupera entidade dos usuarios
        /// </summary>
        /// <param name="entidadeUsuarios"></param>
        /// <param name="usuario"></param>
        /// <returns></returns>
        private List<IdentityUser> RecuperaUsuarios(List<IdentityUser> entidadeUsuarios, IdentityUser usuario)
        {
            // recupera Usuarios
            entidadeUsuarios = entidadeUsuarios.Select(item =>
            {
                // Recupera todos menos o do criador
                if (item.Id != usuario.Id)
                    item = this.userManager.FindByNameAsync(item.UserName).Result;

                return item;
            }).ToList();

            entidadeUsuarios.Add(usuario);
            entidadeUsuarios = entidadeUsuarios.DistinctBy(x => x.UserName).ToList();

            return entidadeUsuarios;
        }

        /// <summary>
        /// Valida permissao do usuario em relação à promoçao que ele deseja fazer
        /// </summary>
        /// <param name="usuario"></param>
        private async Task ValidaUsuarioPermissaoAsync(int grupoId, Usuario usuario)
        {
            IdentityUser usuarioAtual = await RecuperaUsuarioAtualAsync();

            // Verifica permissao do usuario atual
            GrupoUsuario grupoUsuario = await this.repositorioGrupoUsuario.ObterAsync(x => x.UsuarioId == usuarioAtual.Id &&
                                                                                            x.GrupoId == grupoId &&
                                                                                            x.Excluido == false);

            if (grupoUsuario == null)
            {
                throw new ApiExcecao(403, "Usuario não pode alterar/adicionar/excluir permissao pois não é membro do grupo");
            }
            else if (usuario.Permissao == API.Grupo.Permissao.Dono) // Se a alteracao for para Dono
            {
                if (grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Dono)
                    throw new ApiExcecao(403, "Usuario não pode promover/adicionar/excluir dono pois não é dono do grupo");
            }
            else if (usuario.Permissao == API.Grupo.Permissao.Moderador) // Se a alteracao for para Moderador
            {
                if (grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Dono &&
                    grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Moderador)
                    throw new ApiExcecao(403, "Usuario não pode promover/adicionar/excluir um moderador pois não é dono ou moderador do grupo");
            }
            else if (usuario.Permissao == API.Grupo.Permissao.Membro) // Se a alteracao for para Membro
            {
                if (grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Dono &&
                    grupoUsuario.PermissaoId != (int)API.Grupo.Permissao.Moderador)
                    throw new ApiExcecao(403, "Usuario não pode promover/adicionar/excluir um membro pois não é dono ou moderador do grupo");
            }

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

        /// <summary>
        /// Recupera usuario pelo Nome
        /// </summary>
        /// <param name="nomeUsuario"></param>
        /// <returns></returns>
        private async Task<IdentityUser> RecuperaUsuarioPeloNome(string nomeUsuario)
        {
            IdentityUser entidadeUsuario = await this.userManager.FindByNameAsync(nomeUsuario);
            if (entidadeUsuario == null)
                throw new ApiExcecao(404, "Usuario não existe");
            return entidadeUsuario;
        }

        #endregion [ Métodos privados ]
    }
}