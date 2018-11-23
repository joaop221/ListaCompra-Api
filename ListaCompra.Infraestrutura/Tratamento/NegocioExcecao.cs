/*
© Copyright 2018 Rede S.A.
Autor   : Frank Wendel R. Galindo
Empresa : Iteris Consultoria e Software
*/

using System;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ListaCompra.Infraestrutura.Tratamento
{
    /// <summary>
    /// </summary>
    public class NegocioExcecao : Exception
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
        /// Inicializa uma instância da classe de exceção de dados. Define a mensagem como <see cref="string.Empty"/>.
        /// </summary>
        public NegocioExcecao() :
            this(string.Empty)
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="mensagem">A mensagem da exceção.</param>
        public NegocioExcecao(string mensagem) :
            this(mensagem, new object[] { })
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="formatoMensagem">O formato da mensagem, conforme utilizado em <see cref="string.Format"/>.</param>
        /// <param name="valores">        
        /// Os valores para preenchimento do formato da mensagem, conforme utilizado em <see cref="string.Format"/>.
        /// </param>
        public NegocioExcecao(string formatoMensagem, params object[] valores) :
            this(default(int), string.Format(formatoMensagem, valores))
        { }

        /// <summary>
        /// Inicializa uma instância da classe de exceção de dados.
        /// </summary>
        /// <param name="codigo">  Código da execeção.</param>
        /// <param name="mensagem">A mensagem da exceção.</param>
        public NegocioExcecao(int codigo, string mensagem) :
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
        public NegocioExcecao(int codigo, string formatoMensagem, params object[] valores) :
            base(string.Format(formatoMensagem, valores))
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
    }
}