using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Modelo.API.Produto;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ListaCompra.API.Controllers
{
    /// <summary>
    /// Produto
    /// </summary>
    [Route("[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProdutoController : ControllerBase
    {
        private readonly NegocioProduto negocioProduto;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="negocioProduto"></param>
        public ProdutoController(NegocioProduto negocioProduto)
        {
            this.negocioProduto = negocioProduto;
        }

        /// <summary>
        /// Obter
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ProdutoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("{id}")]
        public async Task<ProdutoResponse> Obter([FromRoute] int id)
            => await this.negocioProduto.Obter(id);

        /// <summary>
        /// Listar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ProdutoResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("")]
        public async Task<List<ProdutoResponse>> Listar()
            => await this.negocioProduto.Listar();

        /// <summary>
        /// Listar usando termo
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ProdutoResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpGet("/[controller]/Listar/{termo}")]
        public async Task<List<ProdutoResponse>> ListarComTermo(string termo)
            => await this.negocioProduto.Listar(termo);

        /// <summary>
        /// Criar
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ProdutoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost]
        public async Task<ProdutoResponse> Criar([FromBody] ProdutoRequest model)
            => await this.negocioProduto.Criar(model);

        /// <summary>
        /// Criar um produto e atrelar ele à uma lista
        /// </summary>
        [SwaggerResponse(200, "Sucesso", typeof(ProdutoResponse))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("/[controller]/Lista/")]
        public async Task<ProdutoResponse> CriarNaLista([FromBody] CriaProdutoComListaRequest model)
            => await this.negocioProduto.Criar(model);

        /// <summary>
        /// Criar varios produtos e atrelar eles à uma lista
        /// </summary>
        /// <returns>Resultado do check</returns>
        [SwaggerResponse(200, "Sucesso", typeof(ProdutoResponse[]))]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPost("/[controller]/Lista/Lote")]
        public async Task<List<ProdutoResponse>> CriarProdutosNaLista([FromBody] CriaListaProdutoComListaRequest model)
            => await this.negocioProduto.Criar(model);

        /// <summary>
        /// Atualizar
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpPut("{id}")]
        public async Task Atualizar([FromRoute] int id, [FromBody] ProdutoRequest model)
            => await this.negocioProduto.Atualizar(id, model);

        /// <summary>
        /// Excluir
        /// </summary>
        [SwaggerResponse(200, "Sucesso")]
        [SwaggerResponse(503, Description = "API ou algum recurso que ela depende está fora do ar")]
        [SwaggerResponse(500, Description = "Erro interno desconhecido")]
        [HttpDelete("{id}")]
        public async Task Excluir([FromRoute] int id)
            => await this.negocioProduto.Excluir(id);
    }
}