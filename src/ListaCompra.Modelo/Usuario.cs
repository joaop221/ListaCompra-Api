using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Modelo de usuário da aplicação
	/// </summary>
    public class Usuario : Base
    {
		/// <summary>
		/// Id do banco do usuário
		/// </summary>
		public int IdUsuario { get; set; }

		/// <summary>
		/// Email do usuário
		/// </summary>
		public String Email { get; set; }

		/// <summary>
		/// NickName do usuário
		/// </summary>
		public String NickName { get; set; }

		/// <summary>
		/// Nome do usuário
		/// </summary>
		public String Nome { get; set; }

		/// <summary>
		/// Senha do usuário
		/// Não estara preenchida sempre
		/// </summary>
		public String Password { get; set; } //TODO: Criar métodos de get e set garantindo criptografia da senha
	}
}
