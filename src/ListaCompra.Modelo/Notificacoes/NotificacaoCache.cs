using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListaCompra.Modelo.Notificacoes
{
	/// <summary>
	/// Cache de notificação visando garantir velocidade da consulta de notificações
	/// </summary>
    public class NotificacaoCache
    {
        #region Métodos Privados

        private IMemoryCache memoryCache;

        private Dictionary<Guid, IEnumerable<int>> chaveUsuario;

        private List<Guid> RetornaChave(int idUsuario)
        {
            return chaveUsuario.Where(x => x.Value.Any(y => y == idUsuario)).Select(x => x.Key).ToList();
        }

        private Guid CriaChave(IEnumerable<int> enumeradores)
        {
            var chave = new Guid();

            while (true)
            {
                if (chaveUsuario.GetValueOrDefault(chave) == default(IEnumerable<int>))
                    break;

                chave = new Guid();
            }

            chaveUsuario.Add(chave, enumeradores);
            return chave;
        }

        #endregion

        /// <summary>
        /// Contrutor da classe de cache de notificações
        /// </summary>
        /// <param name="memoryCache"></param>
        public NotificacaoCache(IMemoryCache memoryCache)
		{
            this.chaveUsuario = new Dictionary<Guid, IEnumerable<int>>();
			this.memoryCache = this.memoryCache ?? memoryCache;
		}

		/// <summary>
		/// Retorna Notificação apartir de usuário
		/// </summary>
		/// <param name="idUsuario">id do usuário</param>
		/// <returns>Notificação</returns>
		public List<Notificacao> ConsultaNotificacao(int idUsuario)
		{
            var notificacoes = new List<Notificacao>();

            foreach(var item in RetornaChave(idUsuario))
            {
                notificacoes.Add(memoryCache.Get<Notificacao>(item));
            }

            return notificacoes;
        }

		/// <summary>
		/// Insere notificação no cache
		/// </summary>
		/// <param name="notificacao">notificação a ser inserida</param>
		public void InsereNotificacao(Notificacao notificacao)
		{
             memoryCache.Set(CriaChave(notificacao.ListaUsuarios.Select(x => x.IdUsuario)), notificacao);
        }

        /// <summary>
        /// Remove notificação do cache
        /// </summary>
        /// <param name="idUsuario">id do usuário</param>
        public void RemoveNotificacao(int idUsuario)
        {
            memoryCache.Remove(idUsuario);
        }
	}
}
