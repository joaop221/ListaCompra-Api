using ListaCompra.Modelo.Enumeradores;
using System.Collections.Generic;

namespace ListaCompra.Modelo.Notificacoes
{
	/// <summary>
	/// Modelo de notificação da aplicação
	/// </summary>
    public class Notificacao : Base
    {
		/// <summary>
		/// Id da Notificação
		/// </summary>
		public int IdNotificacao { get; set; }

		/// <summary>
		/// Titulo da Notificação
		/// </summary>
		public string Titulo { get; set; }

		/// <summary>
		/// Texto da Notificação
		/// </summary>
		public string Texto { get; set; }

		/// <summary>
		/// Tipo de Notificacao
		/// </summary>
		public TipoNotificacao TipoNotificacao { get; set; }

		/// <summary>
		/// Lista de usuários que receberão a notificação
		/// </summary>
		public List<Usuario> ListaUsuarios { get; set; }

		/// <summary>
		/// Registra quais usuários estão pendentes de envio da notificação
		/// </summary>
		public Dictionary<int, bool> Enviado { get; set; }

		/// <summary>
		/// Verifica se status da notificação é concluído
		/// </summary>
		public bool Concluida
		{
			get => Enviado.ContainsValue(false);
		}
	}
}
