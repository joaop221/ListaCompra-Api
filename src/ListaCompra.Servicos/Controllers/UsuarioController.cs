using System.Linq;
using ListaCompra.Modelo;
using ListaCompra.Modelo.Response;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ListaCompra.Servicos.Controllers
{
	/// <summary>
	/// Controller de Usuário
	/// </summary>
	[Route("api/[controller]")]
	public class UsuarioController : BaseController
	{
		private NegocioUsuario negocioUsuario;

		/// <summary>
		/// Construtor do controller
		/// </summary>
		public UsuarioController()
		{
			this.negocioUsuario = new NegocioUsuario();
		}

		/// <summary>
		/// Método padrão de criação de usuário
		/// </summary>
		/// <param name="usuario">Usuário a ser criado</param>
		/// <returns>Grupo padrão do usuário</returns>
		[SwaggerResponse(200, Type = typeof(ItemResponse<Grupo>))]
		[SwaggerResponse(400, Type = typeof(ItemResponse<Grupo>))]
		[SwaggerResponse(500, Type = typeof(ItemResponse<Grupo>))]
		[HttpPost(template: "criar")]
		public IActionResult Criar([FromBody]Usuario usuario)
		{
			return ExecucaoTratada<ItemResponse<Grupo>>((retorno) =>
			{
				retorno.Item = this.negocioUsuario.Criar(usuario);
                retorno.CodigoRetorno = 200;
                retorno.Messagem = "Usuário criado com sucesso";
			});
		}

        /// <summary>
        /// Método padrão de login de usuário
        /// </summary>
        /// <param name="usuario">Usuário</param>
        /// <param name="senha">Senha</param>
        /// <returns>Se sucesso Lista de Grupos de usuário</returns>
        [SwaggerResponse(200, Type = typeof(ListResponse<Grupo>))]
        [SwaggerResponse(400, Type = typeof(ListResponse<Grupo>))]
        [SwaggerResponse(500, Type = typeof(ListResponse<Grupo>))]
        [HttpGet(template: "login")]
        public IActionResult Login(string usuario, string senha)
        {
            return ExecucaoTratada<ListResponse<Grupo>>((retorno) => {
                retorno.Item = this.negocioUsuario.Login(usuario, senha);
                retorno.CodigoRetorno = 200;
                retorno.Messagem = "Login realizado com sucesso";
            });
        }
    }
}