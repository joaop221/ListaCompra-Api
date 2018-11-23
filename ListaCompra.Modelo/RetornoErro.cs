namespace ListaCompra.Modelo
{
    /// <summary>
    /// classe de erro
    /// </summary>
    public class RetornoErro
    {
        /// <summary>
        /// Construtor padrão da classe
        /// </summary>
        public RetornoErro() { }

        /// <summary>
        /// Construtor completo
        /// </summary>
        public RetornoErro(string msg, int? codigo, string fonte)
        {
            this.CodigoRetorno = codigo;
            this.FonteErro = fonte;
            this.MensagemRetorno = msg;
        }

        /// <summary>
        /// Construtor para erros de validação
        /// </summary>
        public RetornoErro(string msg, int? codigo)
        {
            this.CodigoRetorno = codigo;
            this.MensagemRetorno = msg;
            this.FonteErro = "Erro na validação do request";
        }

        /// <summary>
        /// Código do Retorno
        /// </summary>
        public int? CodigoRetorno { get; set; }

        /// <summary>
        /// Mensagem do Retorno
        /// </summary>
        public string MensagemRetorno { get; set; }

        /// <summary>
        /// Mensagem do Retorno
        /// </summary>
        public string FonteErro { get; set; }
    }
}