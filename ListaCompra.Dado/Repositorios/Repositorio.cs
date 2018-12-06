using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Modelo.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Z.EntityFramework.Plus;

namespace ListaCompra.Dado.Repositorios
{
    public class Repositorio<TEntidade> : RepositorioBase, IRepositorio<TEntidade>
                where TEntidade : Entidade

    {
        protected readonly BDContextoBase Db;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IServiceProvider services;

        public Repositorio(BDContextoBase contexto, IServiceProvider services)
        {
            this.Db = contexto;
            this.services = services;

            this.httpContextAccessor = this.services.GetRequiredService<IHttpContextAccessor>();
            this.userManager = this.services.GetRequiredService<UserManager<IdentityUser>>();
        }

        #region [ Métodos de Inclusão ]

        /// <summary>
        /// Inclui o item no banco
        /// </summary>
        /// <returns>Objeto</returns>
        public async Task<TEntidade> InserirAsync(TEntidade item, int? setTimeoutTo = null, int reattempts = 0)
        {
            var usuarioAtual = await ObterUsuarioAtualAsync();

            //Verifica se existe algum item
            if (item == null)
                return item;

            //Define os valores padrões do item
            DefinirValorPadrao(item, OperacaoBanco.Inserir, usuarioAtual);

            this.Db.Set<TEntidade>().Add(item);
            this.Db.Entry(item).State = EntityState.Added;

            try
            {
                var validationContext = new ValidationContext(item);
                Validator.ValidateObject(item, validationContext);

                this.Db.SaveChanges();
            }
            catch (Exception)
            {
                if (reattempts > 3)
                    throw;

                return await InserirAsync(item, setTimeoutTo, ++reattempts);
            }

            return item;
        }

        /// <summary>
        /// Efetua a inclusão da lista enviada
        /// </summary>
        /// <param name="type">lista de itens</param>
        /// <returns>Objeto</returns>
        public async Task InserirAsync(List<TEntidade> itens, int? setTimeoutTo = null, int reattempts = 0)
        {
            var usuarioAtual = await ObterUsuarioAtualAsync();

            //Verifica se possui item na lista
            if (itens == null || itens.Count == 0)
                return;

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Db.Database.SetCommandTimeout(setTimeoutTo ?? 0);

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();

            //Preenche os campos padrões
            itens.ForEach(i => DefinirValorPadrao(i, OperacaoBanco.Inserir, usuarioAtual));

            //Inclui o range
            tabela.AddRange(itens);

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception)
            {
                if (reattempts > 3)
                    throw;

                await InserirAsync(itens, setTimeoutTo, ++reattempts);
            }
        }

        #endregion [ Métodos de Inclusão ]

        #region [ Atualizar ]

        /// <summary>
        /// Efetua a atualização do objeto informado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <returns>Objeto</returns>
        public async Task AtualizarAsync(TEntidade item, bool exclusaoLogica = false, int reattempts = 0)
        {
            var usuarioAtual = await ObterUsuarioAtualAsync();

            try
            {
                //Define os valores padrões do item
                DefinirValorPadrao(item, OperacaoBanco.Atualizar, usuarioAtual);

                //Define o estado como modificado
                this.Db.Entry(item).State = EntityState.Modified;

                try
                {
                    this.Db.SaveChanges();
                }
                catch (Exception)
                {
                    if (reattempts > 3)
                        throw;

                    await AtualizarAsync(item, exclusaoLogica, ++reattempts);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Atualiza a lista de objetos informados
        /// </summary>
        /// <typeparam name="T">Tipo de objeto para ser atualizado</typeparam>
        /// <param name="itens">Lista de entidades</param>
        /// <returns></returns>
        public async Task AtualizarAsync(List<TEntidade> itens, bool exclusaoLogica = false, int reattempts = 0)
        {
            var usuarioAtual = await ObterUsuarioAtualAsync();

            //Verifica se possui item na lista
            if (itens == null || itens.Count == 0)
                return;

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            //Atualiza todos os itens
            itens.ForEach(item =>
                {
                    // Define o estado como modificado
                    this.Db.Entry(item).State = EntityState.Modified;

                    //Define os valores padrões do item
                    DefinirValorPadrao(item, exclusaoLogica ? OperacaoBanco.ExcluirLogico : OperacaoBanco.Atualizar, usuarioAtual);
                });

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception)
            {
                if (reattempts > 3)
                    throw;

                await AtualizarAsync(itens, exclusaoLogica, ++reattempts);
            }
        }

        #endregion [ Atualizar ]

        #region [ Métodos para Excluir o item ]

        /// <summary>
        /// Exclui o objeto solicitado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <returns>Objeto</returns>
        public async Task ExcluirAsync(TEntidade item, bool exclusaoFisica = false, int reattempts = 0)
        {
            var usuarioAtual = await ObterUsuarioAtualAsync();

            try
            {
                //Verifica se deve excluir físicamente o registro
                if (exclusaoFisica)
                {
                    //Define o estado como modificado
                    this.Db.Entry(item).State = EntityState.Deleted;

                    try
                    {
                        this.Db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        if (reattempts > 3)
                            throw;

                        await ExcluirAsync(item, exclusaoFisica, ++reattempts);
                    }
                }
                else
                {
                    //Define os valores padrões do item
                    DefinirValorPadrao(item, OperacaoBanco.ExcluirLogico, usuarioAtual);

                    //Exclusão lógica do item
                    await AtualizarAsync(item, true);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Exclui a lista de objetos informados
        /// </summary>
        /// <param name="itens">Lista de entidades</param>
        /// <returns>Objeto</returns>
        public async Task ExcluirAsync(List<TEntidade> itens, bool exclusaoFisica = false)
        {
            //Verifica se possui item na lista
            if (itens == null || itens.Count == 0)
                return;

            if (exclusaoFisica)
                itens.ForEach(async i => await ExcluirAsync(i, true));
            else
                await AtualizarAsync(itens, true);
        }

        /// <summary>
        /// Exclui os objetos com as chaves informadas
        /// </summary>
        /// <typeparam name="T">Tipo de entidade de banco</typeparam>
        /// <param name="entidade">      Entidade</param>
        /// <param name="chaves">        Lista de chaves para exclusão</param>
        /// <param name="exclusaoFisica">Tipo de exclusão</param>
        public async Task ExcluirAsync(bool exclusaoFisica = false, params object[] chaves)
        {
            if (chaves.Length == 0)
                return;

            //Efetua a exclusão dos itens

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();
            var itens = new List<TEntidade>();

            foreach (var i in chaves)
            {
                TEntidade entidade = tabela.Find(i);
                if (entidade != null)
                    itens.Add(entidade);
            }

            //Exclui todos os itens
            if (itens.Count > 0)
                await ExcluirAsync(itens, exclusaoFisica);
        }

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Lista de objetos encontrados</returns>
        public async Task ExcluirAsync(Expression<Func<TEntidade, bool>> filtro, bool exclusaoFisica = false)
        {
            var usuarioAtual = await ObterUsuarioAtualAsync();

            //Busca o objeto solicitado

            if (exclusaoFisica)
                this.Db.Set<TEntidade>().Where(filtro).Delete();
            else
                this.Db.Set<TEntidade>().Where(filtro).Update(i =>
                                                        (TEntidade)new Entidade()
                                                        {
                                                            LoginAlteracao = usuarioAtual,
                                                            DataAlteracao = DateTime.Now,
                                                            Excluido = true
                                                        }
                                                    );
        }

        #endregion [ Métodos para Excluir o item ]

        #region [ Métodos Obter ]

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <returns>Objeto</returns>
        public async Task<TEntidade> ObterAsync(object chave)
        {
            var retorno = default(TEntidade);

            //Busca o objeto solicitado
            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();
            retorno = await tabela.FindAsync(chave);

            //Retorna
            return retorno;
        }

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <param name="carregarEntidades">Entidades que devem ser carregadas na consulta</param>
        /// <returns>Objeto</returns>
        public async Task<TEntidade> ObterAsync(Expression<Func<TEntidade, bool>> filtro, ListaEntidade<TEntidade> carregarEntidades = null)
        {
            var retorno = default(TEntidade);

            //Busca o objeto solicitado

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            IQueryable<TEntidade> tabela = this.Db.Set<TEntidade>().Where(filtro);

            if (carregarEntidades != null && carregarEntidades.Itens != null && carregarEntidades.Itens.Any())
                foreach (Expression<Func<TEntidade, IEntidade>> path in carregarEntidades.Itens)
                    tabela = tabela.Include(path);

            retorno = await tabela.FirstOrDefaultAsync();

            //Retorna
            return retorno;
        }

        #endregion [ Métodos Obter ]

        #region [ Métodos Listar ]

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <param name="carregarEntidades">Entidades que devem ser carregadas na consulta</param>
        /// <returns>Lista de objetos encontrados</returns>
        public async Task<List<TEntidade>> ConsultarOrdenadoAsync<TKey>(Expression<Func<TEntidade, bool>> filtro, Expression<Func<TEntidade, TKey>> ordenacao, ListaEntidade<TEntidade> carregarEntidades = null, bool ascendente = true)
        {
            //Busca o objeto solicitado

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            IQueryable<TEntidade> tabela = null;

            if (filtro == null)
                tabela = this.Db.Set<TEntidade>().AsQueryable();
            else
                tabela = this.Db.Set<TEntidade>().Where(filtro);

            //Ordena os itens
            if (ordenacao != null)
                if (!ascendente)
                    tabela = tabela.OrderByDescending(ordenacao);
                else
                    tabela = tabela.OrderBy(ordenacao);

            if (carregarEntidades != null && carregarEntidades.Itens != null && carregarEntidades.Itens.Any())
                foreach (Expression<Func<TEntidade, IEntidade>> path in carregarEntidades.Itens)
                    tabela = tabela.Include(path);

            //Retorna
            return await tabela.ToListAsync();
        }

        /// <summary>
        /// Retorna a lista de objetos
        /// </summary>
        /// <returns>Lista de objetos na base</returns>
        public async Task<List<TEntidade>> ConsultarAsync(params object[] chaves)
        {
            var retorno = new List<TEntidade>();

            //Busca o objeto solicitado

            chaves.ToList().ForEach(async i =>
            {
                TEntidade entidade = await ObterAsync(i);
                if (entidade != null)
                    retorno.Add(entidade);
            });

            //Retorna
            return await Task.FromResult(retorno);
        }

        public async Task<List<TEntidade>> ConsultarAsync(Expression<Func<TEntidade, bool>> filtro, ListaEntidade<TEntidade> carregarEntidades = null, int paginaAtual = -1, int itensPagina = -1, bool ascendente = true)
        {
            //Busca o objeto solicitado

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            IQueryable<TEntidade> tabela = null;

            if (filtro == null)
                tabela = this.Db.Set<TEntidade>().AsQueryable();
            else
                tabela = this.Db.Set<TEntidade>().Where(filtro);

            if (carregarEntidades != null && carregarEntidades.Itens != null && carregarEntidades.Itens.Any())
                foreach (Expression<Func<TEntidade, IEntidade>> path in carregarEntidades.Itens)
                    tabela = tabela.Include(path);

            //Efetua a busca no banco
            if (paginaAtual > -1 && itensPagina > -1)
                tabela = tabela.Skip(paginaAtual * itensPagina).Take(itensPagina);

            //Retorna
            return await tabela.ToListAsync();
        }

        /// <summary>
        /// Retorna a quantidade de objetos encontrados com o filtro informado
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Quantidade de objetos encontrados</returns>
        public async Task<int> ConsultarQtdAsync(Expression<Func<TEntidade, bool>> filtro)
        {
            var retorno = 0;

            //Busca o objeto solicitado
            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();

            //Efetua a busca dos itens
            if (filtro != null)
                retorno = tabela.Where(filtro).Count();
            else
                retorno = tabela.Count();

            return await Task.FromResult(retorno);
        }

        #endregion [ Métodos Listar ]

        #region [ Métodos Auxiliares ]

        /// <summary>
        /// Retorna o usuário atual que está realizando a operação
        /// </summary>
        /// <returns></returns>
        protected async Task<string> ObterUsuarioAtualAsync()
        {
            IdentityUser logado = await this.userManager.GetUserAsync(this.httpContextAccessor.HttpContext.User);
            return logado != null ? logado.UserName : string.Concat(Environment.UserDomainName, @"\", Environment.UserName);
        }

        #endregion [ Métodos Auxiliares ]
    }
}