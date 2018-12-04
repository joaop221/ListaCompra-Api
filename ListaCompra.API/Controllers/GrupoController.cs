using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Modelo.API.Grupo;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    /// <summary>
    /// Grupo
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class GrupoController : ControllerBase
    {
        private readonly NegocioGrupo negocioGrupo;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="negocioGrupo"></param>
        public GrupoController(NegocioGrupo negocioGrupo)
        {
            this.negocioGrupo = negocioGrupo;
        }

        /// <summary>
        /// Obter
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<GrupoResponse> Obter([FromRoute] int id)
            => await this.negocioGrupo.Obter(id);

        /// <summary>
        /// Listar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("Listar")]
        public async Task<List<GrupoResponse>> Listar()
            => await this.negocioGrupo.Listar();

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(GrupoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<GrupoResponse> Criar([FromBody] GrupoRequest model)
            => await this.negocioGrupo.Criar(model);

        /// <summary>
        /// Atualizar
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{id}")]
        public async Task Atualizar([FromRoute] int id, [FromBody] GrupoRequest model)
            => await this.negocioGrupo.Atualizar(id, model);

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{id}")]
        public async Task Excluir([FromRoute] int id)
            => await this.negocioGrupo.Excluir(id);
    }
}