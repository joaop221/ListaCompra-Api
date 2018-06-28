using System;
using System.Collections.Generic;

namespace ListaCompra.Dados.Banco.Contexto
{
	/// <summary>
	/// Contexto de Transações do banco de dados
	/// </summary>
    public sealed class ContextoTransacional : IDisposable
    {
		/// <summary>
		/// Coleção de transações em execução no banco de dados
		/// </summary>
        [ThreadStatic]
        public static Dictionary<String, ContextoDB> TransacoesDB = null;

        /// <summary>
        /// Define se a instância já foi destruida.
        /// </summary>
        private Boolean disposed;

        /// <summary>Inicia o contexto transacional da aplicação (Só pode ser iniciado através do PortalWeb)</summary>
        public ContextoTransacional()
        {
            TransacoesDB = new Dictionary<String, ContextoDB>();
        }

        /// <summary>
        /// Executa a confirmação de mudancas.
        /// </summary>
        public void ConfirmarMudancas()
        {
            // Realizando o commit da transação
            if (TransacoesDB == null)
                return;

            //Efetua o commit de todas as transações no contexto
            ExecutarAcaoContexto(c =>
            {
                if (c.Database.CurrentTransaction != null)
                    c.Database.CurrentTransaction.Commit();

                c.ExecutandoTransacao = false;
                c.Dispose();
                c = null;
            });

            TransacoesDB = null;
        }

        /// <summary>
        /// Executa o cancelamento de mudancas.
        /// </summary>
        public void CancelarMudancas()
        {
            if (TransacoesDB == null)
                return;

            //Efetua o commit de todas as transações no contexto
            ExecutarAcaoContexto(c =>
            {
                if (c.Database.CurrentTransaction != null)
                    c.Database.CurrentTransaction.Rollback();

                c.ExecutandoTransacao = false;
                c.Dispose();
                c = null;
            });

            TransacoesDB = null;
        }

        #region [ Métodos Auxiliares ]
        /// <summary>Inicia uma transação no banco de dados</summary>
        /// <returns>Cria uma transação e retorna</returns>
        internal static ContextoDB AdicionarTransacao(Type contextType)
        {
            if (TransacoesDB == null)
                TransacoesDB = new Dictionary<String, ContextoDB>();

            //Busca o contexto na lista, se já existir, não adiciona
            ContextoDB db = BuscarContexto(contextType);
            if (db != null)
                return db;

            //Inicia a transação
            db = (ContextoDB)Activator.CreateInstance(contextType);
            db.Database.BeginTransaction();
            db.ExecutandoTransacao = true;

            //Adiciona na lista de objeto transacional
            TransacoesDB.Add(db.GetType().FullName, db);

            return db;
        }

        /// <summary>Busca o objeto de contexto de transação com o banco de dados</summary>
        /// <returns></returns>
        internal static ContextoDB BuscarContexto(Type contextType)
        {
            if (TransacoesDB == null || !TransacoesDB.ContainsKey(contextType.FullName))
                return null;
            return TransacoesDB[contextType.FullName];
        }

        /// <summary>Executa a ação para todos os objectos do contexto</summary>
        /// <param name="acao">Ação</param>
        private void ExecutarAcaoContexto(Action<ContextoDB> acao)
        {
            foreach (var item in TransacoesDB)
                acao(item.Value);
        }

        /// <summary>
        /// Implementa o método de dstruição do objeto de banco de dados do Entity Framework.
        /// </summary>
        private void DestruirObjetoBanco()
        {
            if (TransacoesDB != null)
            {
                ExecutarAcaoContexto(c =>
                {
                    try
                    {
                        if (c.Database.CurrentTransaction != null)
                            c.Database.CurrentTransaction.Rollback();
                    }
                    // Ocultando erros para casos onde a transação já sofreu rollback.
                    catch { }

                    c.ExecutandoTransacao = false;
                    c.Dispose();
                    c = null;
                });
            }

            TransacoesDB = null;
        }

        /// <summary>
        /// Realiza a destruição da instância.
        /// </summary>
        ~ContextoTransacional()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Realiza a destruição da instância.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Realiza a destruição da instância.
        /// </summary>
        /// <param name="disposing">Define se a destruição foi realizada explicitamente.</param>
        internal void Dispose(bool disposing)
        {
            // Verificando se o objeto já está em processo de liberação
            if (this.disposed)
                return;
            disposed = true;

            // Realizando a liberação do objeto e da sessão de banco de dados
            DestruirObjetoBanco();
        }
        #endregion
    }
}