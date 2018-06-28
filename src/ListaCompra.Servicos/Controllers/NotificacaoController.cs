using ListaCompra.Modelo.Notificacoes;
using ListaCompra.Modelo.Response;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompra.Servicos.Controllers
{
	/// <summary>
	/// Controller de métodos relacionados a Notificações
	/// </summary>
	[Route("api/[controller]")]
	public class NotificacaoController : BaseController
    {
		private NegocioNotificacao negocioNotificacao;

		/// <summary>
		/// Contrutor do controller de notificações
		/// </summary>
		public NotificacaoController()
		{
			this.negocioNotificacao = new NegocioNotificacao();
		}

        /// <summary>
		/// Consulta de Notificacoes
		/// </summary>
		/// <param name="id">ID do usuário</param>
		/// <returns>Hello</returns>
		[HttpGet(template: "get")]
        public IActionResult Get(int id)
        {
            return ExecucaoTratada<ListResponse<Notificacao>>((retorno) => {
                retorno.Item = negocioNotificacao.Consultar(id);
                retorno.Messagem = "Notificações consultadas com sucesso";
                retorno.CodigoRetorno = 200;
            });
        }
    }
}
