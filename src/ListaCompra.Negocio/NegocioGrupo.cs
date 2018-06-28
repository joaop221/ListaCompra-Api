using ListaCompra.Modelo;
using ListaCompra.Modelo.Enumeradores;
using ListaCompra.Modelo.Notificacoes;
using ListaCompra.Modelo.Tratamento;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Negocio
{
	/// <summary>
	/// Métodos de negócio de grupos
	/// </summary>
	public class NegocioGrupo : NegocioBase
	{
        private NotificacaoCache notificacaoCache;

        private IMemoryCache memoryCache;

        /// <summary>
        /// Contrutor da classe de negócio de grupo
        /// </summary>
        public NegocioGrupo() : base("Grupo")
		{
            this.notificacaoCache = new NotificacaoCache(memoryCache);

        }

        /// <summary>
        /// Método de validação e criação de novo grupo
        /// </summary>
        /// <param name="grupo">Grupo a ser criado</param>
        /// <returns>Id do grupo</returns>
        public int Criar(Grupo grupo)
		{
			var retorno = 2;

			if (retorno != default(int))
			{
                var enviado = new Dictionary<int, bool>();

                foreach(var usuario in grupo.Usuarios)
                    enviado.Add(usuario.IdUsuario, usuario.PreferenciaNotificacao > TipoNotificacao.Importantes);

                var notificacao = new Notificacao()
                {
                    Titulo = "Novo Grupo Criado",
                    Texto = string.Format("Você agora pertence ao grupo {0}", grupo.Nome),
                    ListaUsuarios = grupo.Usuarios,
                    Enviado = enviado,
                    TipoNotificacao = TipoNotificacao.Atualizacoes
                };

                notificacaoCache.InsereNotificacao(notificacao);

                return retorno;
			}

			throw new NegocioExcacao(500, "Falha na criação do usuário");
		}
	}
}
