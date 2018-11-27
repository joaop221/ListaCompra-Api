using System.Threading.Tasks;
using ListaCompra.Modelo.API.Account;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContaController : ControllerBase
    {
        private readonly NegocioConta negocioConta;

        public ContaController(NegocioConta negocioConta)
        {
            this.negocioConta = negocioConta;
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(RegistroResponse))]
        [SwaggerResponse(422, "Erro de validação", typeof(IdentityError[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<LoginResponse> Login([FromBody] LoginRequest model)
            => await this.negocioConta.Login(model);

        /// <summary>
        /// Registro
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(RegistroResponse))]
        [SwaggerResponse(422, "Erro de validação", typeof(IdentityError[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<RegistroResponse> Registro([FromBody] RegistroRequest model)
            => await this.negocioConta.Registro(model);
    }
}