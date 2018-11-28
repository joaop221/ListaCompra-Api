using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Infraestrutura.Filtros;
using ListaCompra.Modelo.API.Categoria;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidacaoFiltro]
    public class CategoriaController : ControllerBase
    {
        private readonly NegocioCategoria negocioCategoria;

        public CategoriaController(NegocioCategoria negocioCategoria)
        {
            this.negocioCategoria = negocioCategoria;
        }

        /// <summary>
        /// Obter
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<CategoriaResponse> Obter([FromRoute] int id)
            => throw new NotImplementedException();

        /// <summary>
        /// Listar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("Listar")]
        public async Task<List<CategoriaResponse>> Listar()
            => throw new NotImplementedException();

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<CategoriaResponse> Criar([FromBody] CriaCategoriaRequest model)
            => throw new NotImplementedException();

        /// <summary>
        /// Atualizar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(CategoriaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{id}")]
        public async Task<CategoriaResponse> Atualizar([FromRoute] int id, [FromBody] AtualizaCategoriaRequest model)
            => throw new NotImplementedException();

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{id}")]
        public async Task Excluir([FromRoute] int id)
            => throw new NotImplementedException();
    }
}