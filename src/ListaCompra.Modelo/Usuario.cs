using FluentValidation.Attributes;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Validator;
using System;
using System.Security.Cryptography;
using System.Text;

namespace ListaCompra.Modelo
{
	/// <summary>
	/// Modelo de usuário da aplicação
	/// </summary>
	[Validator(typeof(UsuarioValidator))]
	public class Usuario : Base
	{
		/// <summary>
		/// Id do banco do usuário
		/// </summary>
		public int IdUsuario { get; set; }

		/// <summary>
		/// NickName do usuário
		/// </summary>
		public string NickName { get; set; }

		/// <summary>
		/// Nome do usuário
		/// </summary>
		public string Nome { get; set; }

        /// <summary>
        /// Imagem de perfil do usuário
        /// </summary>
        public string Imagem { get; set; }

        /// <summary>
        /// Preferencia de recebimento de notificacoes
        /// </summary>
        public TipoNotificacao PreferenciaNotificacao { get; set; }
    }
}