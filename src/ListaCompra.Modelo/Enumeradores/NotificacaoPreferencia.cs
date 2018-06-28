using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ListaCompra.Modelo.Enumeradores
{
	/// <summary>
	/// Modelo representante de preferências de notificação
	/// </summary>
	public enum TipoNotificacao
    {
		/// <summary>
		/// Nenhuma notificação
		/// </summary>
		[Description("Nenhuma notificação")]
		NaoDefinido = 0,

		/// <summary>
		/// Atualizações do usuário
		/// </summary>
		[Description("Atualizações do usuário")]
		Importantes = 1,

		/// <summary>
		/// Atualizações importantes no grupo e no usuário
		/// </summary>
		[Description("Atualizações importantes no grupo e no usuário")]
		Atualizacoes = 2,

		/// <summary>
		/// Todas as notificações possíveis
		/// </summary>
		[Description("Todas as notificações possíveis")]
		Todas = 3
	}
}
