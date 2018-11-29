using System.Threading.Tasks;
using ListaCompra.Modelo;
using ListaCompra.Modelo.API.Conta;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    /// <summary>
    /// Conta
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ContaController : ControllerBase
    {
        private readonly NegocioConta negocioConta;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="negocioConta"></param>
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

        /// <summary>
        /// Deletar
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(LoginRequest))]
        [SwaggerResponse(422, "Erro de validação", typeof(IdentityError[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete]
        public async Task<RetornoErro> Deleta([FromBody] LoginRequest model)
            => await this.negocioConta.ExcluirConta(model);
    }
}