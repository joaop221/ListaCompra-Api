using System;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Modelo base e comum de todas as entidades da aplicação
	/// </summary>
    public class Base
	{
		/// <summary>
		/// Último usuário a atualizar entidade
		/// </summary>
		public String UsuarioAtualizacao { get; set; }

		/// <summary>
		/// Data da última alteração da entidade
		/// </summary>
		public DateTime DataAtualizacao { get; set; }

		/// <summary>
		/// Criador da entidade
		/// </summary>
		public String UsuarioCriacao { get; set; }

		/// <summary>
		/// Data da criação da entidade
		/// </summary>
		public DateTime DataCriacao { get; set; }
	}
}
