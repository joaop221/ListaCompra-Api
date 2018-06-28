using System;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Modelo base e comum de todas as entidades da aplicação
	/// </summary>
	public abstract class Base
	{
		/// <summary>
		/// Último usuário a atualizar entidade
		/// </summary>
		protected virtual string UsuarioAtualizacao { get; set; }

		/// <summary>
		/// Data da última alteração da entidade
		/// </summary>
		protected virtual DateTime DataAtualizacao { get; set; }

		/// <summary>
		/// Criador da entidade
		/// </summary>
		protected virtual string UsuarioCriacao { get; set; }

		/// <summary>
		/// Data da criação da entidade
		/// </summary>
		protected virtual DateTime DataCriacao { get; set; }
	}
}