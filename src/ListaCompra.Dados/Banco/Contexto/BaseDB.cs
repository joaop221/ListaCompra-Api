using System;

namespace ListaCompra.Dados.Banco.Contexto
{
    /// <summary>
	/// Define o a operação realizada no banco.
	/// </summary>
    public enum OperacaoBanco
    {
        /// <summary>
		/// Operação de inclusão no banco de dados.
		/// </summary>
        Inserir = 1,

        /// <summary>
		/// Operação de atualização no banco de dados.
		/// </summary>
        Atualizar = 2,

        /// <summary>
		/// Operação de exclusão lógica no banco de dados.
		/// </summary>
        ExcluirLogico = 3,

        /// <summary>
		/// Operação de exclusão física no banco de dados.
		/// </summary>
        ExcluirFisico = 4,

        /// <summary>
		/// Operação de consulta de um registro no banco de dados.
		/// </summary>
        Obter = 5,

        /// <summary>
		/// Operação de consulta no banco de dados.
		/// </summary>
        Consultar = 6
    }

	/// <summary>
	/// Classe Base do contexto de acesso ao banco
	/// </summary>
    public class BaseDB
    {
        private Type dbContextType = null;

        /// <summary>
		/// Contexto de conexão EF utilizado pela entidade
		/// </summary>
        public Type DBContextType
        {
            get
            {
                return typeof(ContextoDB);
            }
        }

		/// <summary>
		/// Construtor vazio
		/// </summary>
        public BaseDB() { }

		/// <summary>
		/// Construtor que define contexto
		/// </summary>
		/// <param name="contextType"></param>
        public BaseDB(Type contextType)
        {
            this.dbContextType = contextType;
        }

        /// <summary>Retorna o contexto do banco de dados</summary>
        /// <param name="usarTransacaoContexto"></param>
        /// <returns></returns>
        public ContextoDB RetornarContextoDB(bool usarTransacaoContexto = true)
        {
            if (usarTransacaoContexto && ContextoTransacional.TransacoesDB != null)
            {
                //Verifica se já existe transação aberta para o contexto
                ContextoDB contextoTransacao = ContextoTransacional.BuscarContexto(DBContextType);
                if (contextoTransacao != null)
                    return contextoTransacao;

                //Caso não exista, cria
                return IniciarTransacaoDB();
            }

            //Cria a instância do contexto com o banco de dados e retorna
			//TODO: Criar método de retorno do contexto do banco de dados
            return null;
        }

        /// <summary>Inicia uma transação no banco de dados</summary>
        /// <returns></returns>
        public ContextoDB IniciarTransacaoDB()
        {
            return ContextoTransacional.AdicionarTransacao(DBContextType);
        }
    }
}
