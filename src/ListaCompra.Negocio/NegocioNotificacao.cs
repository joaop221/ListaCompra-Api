using ListaCompra.Modelo.Notificacoes;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Métodos de negócio de notificações
	/// </summary>
	public class NegocioNotificacao : NegocioBase
    {
        private NotificacaoCache notificacaoCache;

        private IMemoryCache memoryCache;

        /// <summary>
        /// Contrutor da classe de negócio de lista de produtos
        /// </summary>
        public NegocioNotificacao() : base("Notificacao")
		{
            this.notificacaoCache = new NotificacaoCache(memoryCache);
		}

        /// <summary>
        /// Consulta Notificações no Id específicado
        /// </summary>
        /// <param name="id">Id do Usuario</param>
        /// <returns>Lista de notificações</returns>
        public List<Notificacao> Consultar(int id)
        {
            return notificacaoCache.ConsultaNotificacao(id);
        }
    }
}
