using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Modelo.Entidades;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace ListaCompra.Dado.Repositorios
{
    public class Repositorio<TEntidade> : RepositorioBase
                where TEntidade : Entidade

    {
        protected readonly BDContextoBase Db;

        public Repositorio(BDContextoBase contexto)
        {
            this.Db = contexto;
        }

        #region [ Métodos de Inclusão ]

        /// <summary>
        /// Inclui o item no banco
        /// </summary>
        /// <returns>Objeto</returns>
        public void Inserir(TEntidade item, int? setTimeoutTo = null, int reattempts = 0)
        {
            //Verifica se existe algum item
            if (item == null)
                return;

            //Define os valores padrões do item
            DefinirValorPadrao(item, OperacaoBanco.Inserir);

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

                Inserir(item, setTimeoutTo, ++reattempts);
            }
        }

        /// <summary>
        /// Efetua a inclusão da lista enviada
        /// </summary>
        /// <param name="type">lista de itens</param>
        /// <returns>Objeto</returns>
        public void Inserir(List<TEntidade> itens, int? setTimeoutTo = null, int reattempts = 0)
        {
            //Verifica se possui item na lista
            if (itens == null || itens.Count == 0)
                return;

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;
            this.Db.Database.SetCommandTimeout(setTimeoutTo ?? 0);

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();

            //Preenche os campos padrões
            itens.ForEach(i => DefinirValorPadrao(i, OperacaoBanco.Inserir));

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

                Inserir(itens, setTimeoutTo, ++reattempts);
            }
        }

        #endregion [ Métodos de Inclusão ]

        #region [ Atualizar ]

        /// <summary>
        /// Efetua a atualização do objeto informado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <returns>Objeto</returns>
        public void Atualizar(TEntidade item, bool exclusaoLogica = false, int reattempts = 0)
        {
            try
            {
                //Define os valores padrões do item
                DefinirValorPadrao(item, OperacaoBanco.Atualizar);

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

                    Atualizar(item, exclusaoLogica, ++reattempts);
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
        public void Atualizar(List<TEntidade> itens, bool exclusaoLogica = false, int reattempts = 0)
        {
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
                    DefinirValorPadrao(item, exclusaoLogica ? OperacaoBanco.ExcluirLogico : OperacaoBanco.Atualizar);
                });

            try
            {
                this.Db.SaveChanges();
            }
            catch (Exception)
            {
                if (reattempts > 3)
                    throw;

                Atualizar(itens, exclusaoLogica, ++reattempts);
            }
        }

        #endregion [ Atualizar ]

        #region [ Métodos para Excluir o item ]

        /// <summary>
        /// Exclui o objeto solicitado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <returns>Objeto</returns>
        public void Excluir(TEntidade item, bool exclusaoFisica = false, int reattempts = 0)
        {
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

                        Excluir(item, exclusaoFisica, ++reattempts);
                    }
                }
                else
                {
                    //Define os valores padrões do item
                    DefinirValorPadrao(item, OperacaoBanco.ExcluirLogico);

                    //Exclusão lógica do item
                    Atualizar(item, true);
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
        public void Excluir(List<TEntidade> itens, bool exclusaoFisica = false)
        {
            //Verifica se possui item na lista
            if (itens == null || itens.Count == 0)
                return;

            if (exclusaoFisica)
                itens.ForEach(i => Excluir(i, true));
            else
                Atualizar(itens, true);
        }

        /// <summary>
        /// Exclui os objetos com as chaves informadas
        /// </summary>
        /// <typeparam name="T">Tipo de entidade de banco</typeparam>
        /// <param name="entidade">      Entidade</param>
        /// <param name="chaves">        Lista de chaves para exclusão</param>
        /// <param name="exclusaoFisica">Tipo de exclusão</param>
        public void Excluir(List<int> chaves, bool exclusaoFisica = false)
        {
            if (chaves.Count == 0)
                return;

            //Efetua a exclusão dos itens

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();
            var itens = new List<TEntidade>();

            chaves.ForEach(i =>
            {
                TEntidade entidade = tabela.Find(i);
                if (entidade != null)
                    itens.Add(entidade);
            }
            );

            //Exclui todos os itens
            if (itens.Count > 0)
                Excluir(itens, exclusaoFisica);
        }

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Lista de objetos encontrados</returns>
        public void Excluir(Expression<Func<TEntidade, bool>> filtro, bool exclusaoFisica = false)
        {
            var usuarioAtual = ObterUsuarioAtual();

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
        public TEntidade Obter(int chave)
        {
            var retorno = default(TEntidade);

            //Busca o objeto solicitado

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();

            retorno = tabela.Find(chave);

            //Retorna
            return retorno;
        }

        /// <summary>
        /// Retorna o objeto solicitado
        /// </summary>
        /// <param name="cod_anexo">ID do item</param>
        /// <returns>Objeto</returns>
        public TEntidade Obter(object[] chave)
        {
            var retorno = default(TEntidade);

            //Busca o objeto solicitado

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            DbSet<TEntidade> tabela = this.Db.Set<TEntidade>();
            retorno = tabela.Find(chave);

            //Retorna
            return retorno;
        }

        #endregion [ Métodos Obter ]

        #region [ Métodos Listar ]

        /// <summary>
        /// Retorna a lista de objetos aplicando um filtro
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Lista de objetos encontrados</returns>
        public List<TEntidade> ConsultarOrdenado<TKey>(Expression<Func<TEntidade, bool>> filtro, Expression<Func<TEntidade, TKey>> ordenacao, bool ascendente = true)
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

            //Retorna
            return tabela.ToList();
        }

        /// <summary>
        /// Retorna a lista de objetos
        /// </summary>
        /// <returns>Lista de objetos na base</returns>
        public List<TEntidade> Consultar(List<int> chaves)
        {
            var retorno = new List<TEntidade>();

            //Busca o objeto solicitado

            chaves.ForEach(i =>
            {
                TEntidade entidade = Obter(i);
                if (entidade != null)
                    retorno.Add(entidade);
            });

            //Retorna
            return retorno;
        }

        public List<TEntidade> Consultar(Expression<Func<TEntidade, bool>> filtro, int paginaAtual = -1, int itensPagina = -1, bool ascendente = true)
        {
            //Busca o objeto solicitado

            this.Db.ChangeTracker.AutoDetectChangesEnabled = false;

            IQueryable<TEntidade> tabela = null;

            if (filtro == null)
                tabela = this.Db.Set<TEntidade>().AsQueryable();
            else
                tabela = this.Db.Set<TEntidade>().Where(filtro);

            //Efetua a busca no banco
            if (paginaAtual > -1 && itensPagina > -1)
                tabela = tabela.Skip(paginaAtual * itensPagina).Take(itensPagina);

            //Retorna
            return tabela.ToList();
        }

        /// <summary>
        /// Retorna a quantidade de objetos encontrados com o filtro informado
        /// </summary>
        /// <param name="filtro">Expressão a ser usada como filtro</param>
        /// <returns>Quantidade de objetos encontrados</returns>
        public int ConsultarQtd(Expression<Func<TEntidade, bool>> filtro)
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

            return retorno;
        }

        #endregion [ Métodos Listar ]

        #region [ Métodos Auxiliares ]

        /// <summary>
        /// Retorna o usuário atual que está realizando a operação
        /// </summary>
        /// <returns></returns>
        protected string ObterUsuarioAtual()
        {
            var usuarioAtual = Thread.CurrentPrincipal.Identity != null && !string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name) ?
                                 Thread.CurrentPrincipal.Identity.Name :
                                  string.Concat(Environment.UserDomainName, @"\", Environment.UserName);
            return usuarioAtual;
        }

        #endregion [ Métodos Auxiliares ]
    }
}