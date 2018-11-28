using System;
using System.Threading.Tasks;
using ListaCompra.Infraestrutura.Filtros;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [ValidacaoFiltro]
    public class GrupoController : ControllerBase
    {
        private readonly NegocioGrupo negocioGrupo;

        public GrupoController(NegocioGrupo negocioGrupo)
        {
            this.negocioGrupo = negocioGrupo;
        }

        /// <summary>
        /// Obter
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(object))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<object> Obter([FromRoute] int id)
            => throw new NotImplementedException();

        /// <summary>
        /// Listar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(object))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("Listar")]
        public async Task<object> Listar([FromBody] object model)
            => throw new NotImplementedException();

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(object))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<object> Criar([FromBody] object model)
            => throw new NotImplementedException();

        /// <summary>
        /// Atualizar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(object))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{id}")]
        public async Task<object> Atualizar([FromRoute] int id, [FromBody] object model)
            => throw new NotImplementedException();

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(object))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{id}")]
        public async Task<object> Excluir([FromRoute] int id)
            => throw new NotImplementedException();
    }
}