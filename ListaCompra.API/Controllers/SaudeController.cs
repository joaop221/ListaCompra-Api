using System.Threading.Tasks;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    /// <summary>
    /// Saude
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class SaudeController : ControllerBase
    {
        private readonly NegocioSaude _negocio;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="negocio"></param>
        public SaudeController(NegocioSaude negocio)
        {
            this._negocio = negocio;
        }

        /// <summary>
        /// Healfcheck
        /// </summary>
        /// <remarks>Testa a conexão com o banco</remarks>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(500, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerOperation(Tags = new[] { "HealfCheck" })]
        [HttpGet]
        [Route("health")]
        public async Task<IActionResult> Check()
        {
            await this._negocio.Check();

            return Ok();
        }
    }
}