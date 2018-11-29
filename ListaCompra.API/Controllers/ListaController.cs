using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Infraestrutura.Filtros;
using ListaCompra.Modelo.API.Lista;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [ValidacaoFiltro]
    public class ListaController : ControllerBase
    {
        private readonly NegocioLista negocioLista;

        public ListaController(NegocioLista negocioLista)
        {
            this.negocioLista = negocioLista;
        }

        /// <summary>
        /// Obter
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ListaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<ListaResponse> Obter([FromRoute] int id)
            => await this.negocioLista.Obter(id);

        /// <summary>
        /// Listar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ListaResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("Listar")]
        public async Task<List<ListaResponse>> Listar()
            => await this.negocioLista.Listar();

        /// <summary>
        /// Criar
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(ListaResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<ListaResponse> Criar([FromBody] ListaRequest model)
            => await this.negocioLista.Criar(model);

        /// <summary>
        /// Atualizar
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{id}")]
        public async Task Atualizar([FromRoute] int id, [FromBody] ListaRequest model)
            => await this.negocioLista.Atualizar(id, model);

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{id}")]
        public async Task Excluir([FromRoute] int id)
            => await this.negocioLista.Excluir(id);
    }
}