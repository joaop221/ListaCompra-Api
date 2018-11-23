/*
© Copyright 2018 Rede S.A.
Autor   : Frank Wendel R. Galindo
Empresa : Iteris Consultoria e Software
*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;

namespace ListaCompra.Infraestrutura.Tratamento
{
    /// <summary>
    /// </summary>
    public class ApiExcecao : Exception
    {
        /// <summary>
        /// Recupera o código que descreve a exceção corrente.
        /// </summary>
        public int Codigo { get; private set; }

        /// <summary>
        /// Recupera uma mensagem amigável com as informações da exception.
        /// </summary>
        public string Detalhes => RecuperaDetalhesException();

        /// <summary>
        /// Status code baseado no código da exceção
        /// </summary>
        public HttpStatusCode StatusCode => GetStatusCode();

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados. Define a mensagem como <see cref="string.Empty"/>.
        /// </summary>
        public ApiExcecao() :
            this(string.Empty)
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="mensagem">A mensagem da exceção.</param>
        public ApiExcecao(string mensagem) :
            this(mensagem, new object[] { })
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="formatoMensagem">O formato da mensagem, conforme utilizado em <see cref="string.Format"/>.</param>
        /// <param name="valores">        
        /// Os valores para preenchimento do formato da mensagem, conforme utilizado em <see cref="string.Format"/>.
        /// </param>
        public ApiExcecao(string formatoMensagem, params object[] valores) :
            this(default(int), string.Format(formatoMensagem, valores))
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="codigo">  Código da execeção.</param>
        /// <param name="mensagem">A mensagem da exceção.</param>
        public ApiExcecao(int codigo, string mensagem) :
            this(codigo, mensagem, new object[] { })
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="codigo">         Código da execeção.</param>
        /// <param name="formatoMensagem">O formato da mensagem, conforme utilizado em <see cref="string.Format"/>.</param>
        /// <param name="valores">        
        /// Os valores para preenchimento do formato da mensagem, conforme utilizado em <see cref="string.Format"/>.
        /// </param>
        public ApiExcecao(int codigo, string formatoMensagem, params object[] valores) :
            base(string.Format(formatoMensagem, valores))
        {
            this.Codigo = codigo;
        }

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="codigo">Código de erro</param>
        /// <param name="ex">    Exceção</param>
        public ApiExcecao(int codigo, Exception ex)
            : base(ex.Message, ex)
        {
            this.Codigo = codigo;
        }

        /// <summary>
        /// Cria uma string amigável com as informações da exception
        /// </summary>
        /// <returns></returns>
        private string RecuperaDetalhesException()
        {
            var st = new StackTrace(this, true);
            var querys = st.GetFrames()
                          .Select(frame => new
                          {
                              LineNumber = frame.GetFileLineNumber(),
                              Method = frame.GetMethod(),
                          });
            var sbRetorno = new StringBuilder();
            foreach (var query in querys)
            {
                sbRetorno.AppendLine(string.Format("{0};; {1} {2}; {3}; {4}.",
                        this.Message,
                        query.Method == null ? string.Empty : string.Format("Método: {0}", query.Method.ToString()),
                        string.Format("Linha: {0} ;", query.LineNumber),
                        this.InnerException == null ? string.Empty : string.Format("InnerException: {0};", this.InnerException),
                        this.StackTrace == null ? string.Empty : string.Format("StackTrace: {0} .", this.StackTrace.TrimStart())
                        ));
            }

            return sbRetorno.ToString();
        }

        /// <summary>
        /// Recupera o status code de um TratamentoExcecao
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>Status code</returns>

        private HttpStatusCode GetStatusCode()
        {
            var status = (HttpStatusCode)this.Codigo;
            if (!Enum.IsDefined(typeof(HttpStatusCode), status))
                return HttpStatusCode.InternalServerError;

            return status;
        }
    }
}