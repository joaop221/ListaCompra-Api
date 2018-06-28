using ListaCompra.Dados.Banco.Contexto;
using ListaCompra.Modelo;
using ListaCompra.Modelo.Tratamento;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ListaCompra.Dados.Banco
{
	/// <summary>
	/// Modelo de banco base
	/// Instancia conexões com banco e mecanismos de acesso importantes
	/// </summary>
	public class RepositorioBase<T> : DbContext where T : Base
	{
		private BaseDB baseDB;

		/// <summary>
		/// Construtor da classe de repositório
		/// </summary>
		public RepositorioBase()
		{
			baseDB = new BaseDB();
		}

		#region [ Métodos de Inclusão ]

		/// <summary>Inclui o item no banco</summary>
		/// <returns>Objeto</returns>
		public void Inserir(T item)
		{
			//Verifica se existe algum item
			if (item == null)
				return;

			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.Set<T>().Add(item);
				db.Entry(item).State = EntityState.Added;

				try
				{
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					throw new DadosExcecao(500, "Erro ao executar acesso ao banco", ex);
				}
			}
		}

		/// <summary>
		/// Efetua a inclusão da lista enviada
		/// </summary>
		/// <param name="itens">Itens a serem inseridos</param>
		/// <returns>Objeto</returns>
		public void Inserir(List<T> itens)
		{
			//Verifica se possui item na lista
			if (itens == null || itens.Count == 0)
				return;

			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				DbSet<T> tabela = db.Set<T>();

				itens.ForEach(i =>
				{
					//Define os valores padrões do item
					tabela.Add(i);
					db.Entry(i).State = EntityState.Added;
				});

				try
				{
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					throw new DadosExcecao(500, "Erro ao executar acesso ao banco", ex);
				}
			}
		}

		#endregion

		#region [ Atualizar ]

		/// <summary>
		/// Efetua a atualização do objeto informado
		/// </summary>
		/// <param name="item">Item a ser atualizado</param>
		/// <param name="exclusaoLogica">Aponta se é ou não exclusão lógica</param>
		/// <returns>Objeto</returns>
		public void Atualizar(T item, bool exclusaoLogica = false)
		{
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				//Define o estado como modificado
				db.Entry(item).State = EntityState.Modified;

				try
				{
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					throw new DadosExcecao(500, "Erro ao executar acesso ao banco", ex);
				}
			}
		}

		/// <summary>
		/// Atualiza a lista de objetos informados
		/// </summary>
		/// <param name="itens">Itens a serem atualizados</param>
		/// <param name="exclusaoLogica">Aponta se é ou não exclusão lógica</param>
		/// <returns></returns>
		public void Atualizar(List<T> itens, bool exclusaoLogica = false)
		{
			//Verifica se possui item na lista
			if (itens == null || itens.Count == 0)
				return;

			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				//Atualiza todos os itens
				itens.ForEach(item =>
				{
					// Define o estado como modificado
					db.Entry(item).State = EntityState.Modified;
				});

				try
				{
					db.SaveChanges();
				}
				catch (Exception ex)
				{
					throw new DadosExcecao(500, "Erro ao executar acesso ao banco", ex);
				}
			}
		}
		#endregion

		#region [ Métodos para Excluir o item ]

		/// <summary>Exclui o objeto solicitado</summary>
		/// <param name="item">Item a ser excluído</param>
		/// <param name="exclusaoFisica">Aponta se é ou não exclusão física</param>
		/// <returns>Objeto</returns>
		public void Excluir(T item, bool exclusaoFisica = false)
		{
			//Verifica se deve excluir físicamente o registro
			if (exclusaoFisica)
			{
				using (ContextoDB db = baseDB.RetornarContextoDB())
				{
					//Define o estado como modificado
					db.Entry(item).State = EntityState.Deleted;

					try
					{
						db.SaveChanges();
					}
					catch (Exception ex)
					{
						throw new DadosExcecao(500, "Erro ao executar acesso ao banco", ex);
					}
				}
			}
			else
			{
				//Exclusão lógica do item
				Atualizar(item, true);
			}
		}

		/// <summary>
		/// Exclui a lista de objetos informados
		/// </summary>
		/// <param name="itens">Itens a serem excluídos</param>
		/// <param name="exclusaoFisica">Aponta se é ou não exclusão física</param>
		/// <returns>Objeto</returns>
		public void Excluir(List<T> itens, bool exclusaoFisica = false)
		{
			//Verifica se possui item na lista
			if (itens == null || itens.Count == 0)
				return;

			//Efetua a exclusão dos itens
			itens.ForEach(i => Excluir(i, exclusaoFisica));
		}

		/// <summary
		/// >Exclui os objetos com as chaves informadas
		/// </summary>
		/// <param name="chaves">Lista de chaves para exclusão</param>
		/// <param name="exclusaoFisica">Tipo de exclusão</param>
		public void Excluir(List<Int32> chaves, bool exclusaoFisica = false)
		{
			if (chaves.Count == 0)
				return;

			//Efetua a exclusão dos itens
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				DbSet<T> tabela = db.Set<T>();
				List<T> itens = new List<T>();

				chaves.ForEach(i =>
				{
					T entidade = tabela.Find(i);
					if (entidade != null)
						itens.Add(entidade);
				}
				);

				//Exclui todos os itens
				if (itens.Count > 0)
					Excluir(itens, exclusaoFisica);
			}
		}

		#endregion

		#region [ Métodos Obter ]

		/// <summary>
		/// Retorna o objeto solicitado
		/// </summary>
		/// <returns>Objeto</returns>
		public T Obter(int chave)
		{
			T retorno = default(T);

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				DbSet<T> tabela = db.Set<T>();

				retorno = tabela.Find(chave);
			}

			//Retorna
			return retorno;
		}

		/// <summary>
		/// Retorna o objeto solicitado
		/// </summary>
		/// <param name="chave">Chave</param>
		/// <returns>Objeto</returns>
		public T Obter(object[] chave)
		{
			T retorno = default(T);

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				DbSet<T> tabela = db.Set<T>();
				retorno = tabela.Find(chave);
			}

			//Retorna
			return retorno;
		}

		/// <summary>
		/// Retorna o objeto solicitado
		/// </summary>
		/// <param name="filtro">Filtro</param>
		/// <param name="carregarEntidades">Entidades que devem ser carregadas na consulta</param>
		/// <returns>Objeto</returns>
		public T Obter(Expression<Func<T, Boolean>> filtro, List<T> carregarEntidades = null)
		{
			T retorno = default(T);

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				var tabela = db.Set<T>().Where(filtro);

				retorno = tabela.FirstOrDefault();
			}

			//Retorna
			return retorno;
		}

		#endregion

		#region [ Métodos Listar ]

		/// <summary>
		/// Retorna a lista de objetos
		/// </summary>
		/// <returns>Lista de objetos na base</returns>
		public List<T> Consultar()
		{
			List<T> retorno = default(List<T>);

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				DbSet<T> tabela = db.Set<T>();
				retorno = tabela.ToList();
			}

			//Retorna
			return retorno;
		}

		/// <summary>
		/// Retorna a lista de objetos
		/// </summary>
		/// <returns>Lista de objetos na base</returns>
		public List<T> Consultar(List<Int32> chaves)
		{
			List<T> retorno = new List<T>();

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				DbSet<T> tabela = db.Set<T>();

				db.ChangeTracker.AutoDetectChangesEnabled = false;

				chaves.ForEach(i =>
				{
					T entidade = tabela.Find(i);
					if (entidade != null)
						retorno.Add(entidade);
				}
				);
			}

			//Retorna
			return retorno;
		}

		/// <summary>
		/// Lista Itens a partir de pesquisa
		/// </summary>
		/// <param name="filtro">Filtro</param>
		/// <param name="carregarEntidades">Carrega todas entidades em memória se True</param>
		/// <param name="paginaAtual">Página atual</param>
		/// <param name="itensPagina">Itens da atual página</param>
		/// <param name="ascendente">Crescente ou decrescente</param>
		/// <returns>Lista de objetos encontrados</returns>
		public List<T> Consultar(Expression<Func<T, Boolean>> filtro, List<T> carregarEntidades = null, Int32 paginaAtual = -1, Int32 itensPagina = -1, Boolean ascendente = true)
		{
			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				IQueryable<T> tabela = null;

				if (filtro == null)
					tabela = db.Set<T>().AsQueryable();
				else
					tabela = db.Set<T>().Where(filtro);

				//Efetua a busca no banco
				if (paginaAtual > -1 && itensPagina > -1)
					tabela = tabela.Skip(paginaAtual * itensPagina).Take(itensPagina);

				//Retorna
				return tabela.ToList();
			}
		}


		/// <summary>
		/// Retorna a lista de objetos aplicando um filtro
		/// </summary>
		/// <param name="filtro">Expressão a ser usada como filtro</param>
		/// <param name="carregarEntidades">Carrega todas entidades em memória se True</param>
		/// <param name="ordenacao">Campo ordenação</param>
		/// <param name="paginaAtual">Página atual</param>
		/// <param name="itensPagina">Itens da atual página</param>
		/// <param name="ascendente">Crescente ou decrescente</param>
		/// <returns>Lista de objetos encontrados</returns>
		public List<T> Consultar<TKey>(Expression<Func<T, Boolean>> filtro, List<T> carregarEntidades, Expression<Func<T, TKey>> ordenacao, Int32 paginaAtual = -1, Int32 itensPagina = -1, Boolean ascendente = true)
		{
			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				IQueryable<T> tabela = null;

				if (filtro == null)
					tabela = db.Set<T>().AsQueryable();
				else
					tabela = db.Set<T>().Where(filtro);

				//Ordena os itens
				if (ordenacao != null)
					if (!ascendente)
						tabela = tabela.OrderByDescending(ordenacao);
					else
						tabela = tabela.OrderBy(ordenacao);

				//Efetua a busca no banco
				if (paginaAtual > -1 && itensPagina > -1)
					tabela = tabela.Skip(paginaAtual * itensPagina).Take(itensPagina);

				//Retorna
				return tabela.ToList();
			}
		}

		/// <summary>
		/// Retorna a lista de objetos aplicando um filtro
		/// </summary>
		/// <param name="funcFiltro">Expressão a ser usada como filtro</param>
		/// <param name="funcOrdenacao"></param>
		/// <param name="paginaAtual">Página atual</param>
		/// <param name="itensPagina">Itens da atual página</param>
		/// <returns>Lista de objetos encontrados</returns>
		public List<T> ConsultarGenerico(Func<IQueryable<T>, IQueryable<T>> funcFiltro, Func<IQueryable<T>, IQueryable<T>> funcOrdenacao, Int32 paginaAtual = -1, Int32 itensPagina = -1)
		{
			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				IQueryable<T> tabela = null;

				tabela = db.Set<T>().AsQueryable();

				//filtro dos itens
				if (funcFiltro != null)
					tabela = funcFiltro(tabela);

				//Ordena os itens
				if (funcOrdenacao != null)
					tabela = funcOrdenacao(tabela);

				//Efetua a busca no banco
				if (paginaAtual > -1 && itensPagina > -1)
					tabela = tabela.Skip(paginaAtual * itensPagina).Take(itensPagina);

				//Retorna
				return tabela.ToList();
			}
		}

		/// <summary>
		/// Retorna a quantidade de objetos encontrados com o filtro informado
		/// </summary>
		/// <param name="filtro">Expressão a ser usada como filtro</param>
		/// <returns>Quantidade de objetos encontrados</returns>
		public Int32 ConsultarQtd(Expression<Func<T, Boolean>> filtro)
		{
			Int32 retorno = 0;

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				DbSet<T> tabela = db.Set<T>();

				//Efetua a busca dos itens
				if (filtro != null)
					retorno = tabela.Where(filtro).Count();
				else
					retorno = tabela.Count();
			}

			return retorno;
		}

		/// <summary>
		/// Retorna quantidade de intens na tabela
		/// </summary>
		/// <param name="funcFiltro">Filtro da busca</param>
		/// <returns>Quantidade de itens</returns>
		public Int32 ConsultarQtdGenerico(Func<IQueryable<T>, IQueryable<T>> funcFiltro)
		{
			Int32 retorno = 0;

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				db.ChangeTracker.AutoDetectChangesEnabled = false;

				DbSet<T> tabela = db.Set<T>();

				//Efetua a busca dos itens
				if (funcFiltro != null)
					retorno = funcFiltro(tabela.AsQueryable()).Count();
				else
					retorno = tabela.Count();
			}


			return retorno;
		}
		#endregion

		#region [ Métodos para execução dentro do contexto ]

		/// <summary>
		/// Executa a solicitação dentro do contexto com retorno
		/// </summary>
		/// <param name="acao">Ação a ser executada</param>
		public void Executar<TContext>(Action<TContext> acao)
			where TContext : ContextoDB
		{
			if (acao == null)
				return;

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				acao((TContext)db);
			}
		}

		/// <summary>
		/// Executa a solicitação dentro do contexto com retorno
		/// </summary>
		/// <typeparam name="TContext">Contexto da execução</typeparam>
		/// <typeparam name="TRetorno">Retorno</typeparam>
		/// <param name="acao">Ação a ser executada</param>
		/// <returns>Retorno da execução</returns>
		public TRetorno Executar<TContext, TRetorno>(Func<TContext, TRetorno> acao)
			where TContext : ContextoDB
		{
			TRetorno retorno = default(TRetorno);

			if (acao == null)
				return retorno;

			//Busca o objeto solicitado
			using (ContextoDB db = baseDB.RetornarContextoDB())
			{
				retorno = acao((TContext)db);
			}

			return retorno;
		}

		#endregion
	}
}
