using System;

namespace ListaCompra.Modelo.Tratamento
{
	/// <summary>
	/// Modelo de exceção de negócio
	/// </summary>
	public class NegocioExcacao : Exception
	{
		/// <summary>
		/// Código status da exceção
		/// </summary>
		public int Codigo { get; }

		/// <summary>
		/// Mensagem amigável
		/// </summary>
		public string Mensagem { get; }

		/// <summary>
		/// Objeto que armazena todas as validações de negócio
		/// </summary>
		public int Validacao { get; set; }

		/// <summary>
		/// Construtor básico de exceção de negócio
		/// </summary>
		public NegocioExcacao(string mensagemAmigavel) : base(mensagemAmigavel)
		{
			this.Mensagem = mensagemAmigavel;
		}

		/// <summary>
		/// Construtor de exceção de negócio
		/// </summary>
		public NegocioExcacao(int codigo, string mensagemAmigavel) : base(mensagemAmigavel)
		{
			this.Codigo = codigo;
			this.Mensagem = mensagemAmigavel;
		}

		/// <summary>
		/// Construtor completo de exceção de negócio
		/// </summary>
		public NegocioExcacao(int codigo, string mensagemAmigavel, Exception innerException)
			: base(mensagemAmigavel, innerException)
		{
			this.Codigo = codigo;
			this.Mensagem = mensagemAmigavel;
		}
	}
}