using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Modelo.API.Categoria;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    /// <summary>
    /// Categoria
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly NegocioCategoria negocioCategoria;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="negocioCategoria"></param>
        public CategoriaController(NegocioCategoria negocioCategoria)
        {
            this.negocioCategoria = negocioCategoria;
        }

        /// <summary>
        /// Obter
        /// </summary>
        /// <remarks>Obter</remarks>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<CategoriaResponse> Obter([FromRoute] int id)
            => await this.negocioCategoria.Obter(id);

        /// <summary>
        /// Listar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("Listar")]
        public async Task<List<CategoriaResponse>> Listar()
            => await this.negocioCategoria.Listar();

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<CategoriaResponse> Criar([FromBody] CategoriaRequest model)
            => await this.negocioCategoria.Criar(model);

        /// <summary>
        /// Atualizar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{id}")]
        public async Task Atualizar([FromRoute] int id, [FromBody] CategoriaRequest model)
            => await this.negocioCategoria.Atualizar(id, model);

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{id}")]
        public async Task Excluir([FromRoute] int id)
            => await this.negocioCategoria.Excluir(id);
    }
}