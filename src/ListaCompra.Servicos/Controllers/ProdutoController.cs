using ListaCompra.Modelo;
using ListaCompra.Modelo.Response;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompra.Servicos.Controllers
{
	/// <summary>
	/// Controller de métodos relacionados a Produtos
	/// </summary>
	[Route("api/[controller]")]
	public class ProdutoController : BaseController
	{
		private NegocioProduto negocioProduto;

		/// <summary>
		/// Contrutor do controller de produtos
		/// </summary>
		public ProdutoController()
		{
			this.negocioProduto = new NegocioProduto();
		}

		/// <summary>
		/// Consulta produto pelo Id
		/// </summary>
		/// <returns>Produto da consulta</returns>
		[HttpGet(template: "get")]
		public IActionResult Get(int id)
		{
			return ExecucaoTratada<ItemResponse<Produto>>((retorno) =>
			{
				//retorno.Item = negocioProduto.Consultar(id);
				retorno.Messagem = "Produto consultada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Consulta produtos pelo Id
		/// </summary>
		/// <returns>Produtos da consulta</returns>
		[HttpGet(template: "list")]
		public IActionResult List(int id)
		{
			return ExecucaoTratada<ListResponse<Produto>>((retorno) =>
			{
				//retorno.Item = negocioProduto.Consultar(id);
				retorno.Messagem = "Produto consultada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Criação de produto
		/// </summary>
		/// <param name="produto">Produto a ser criada</param>
		/// <returns>Id do produto</returns>
		[HttpPost(template: "post")]
		public IActionResult Post([FromBody]Produto produto)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				//retorno.Item = negocioProduto.Consultar(id);
				retorno.Messagem = "Produto criada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Atualização de produto
		/// </summary>
		/// <param name="produto">Produto a ser atualizada</param>
		/// <returns></returns>
		[HttpPut(template: "put")]
		public IActionResult Put([FromBody]Produto produto)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				//retorno.Item = negocioProduto.Consultar(id);
				retorno.Messagem = "Produto atualizada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Deleta Produto pelo Id
		/// </summary>
		/// <param name="id">Id da Produto a ser deletada</param>
		/// <returns>True se sucesso</returns>
		[HttpDelete(template: "delete")]
		public IActionResult Delete(int id)
		{
			return ExecucaoTratada<ItemResponse<bool>>((retorno) =>
			{
				//retorno.Item = negocioProduto.Consultar(id);
				retorno.Messagem = "Produto deletada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}
	}
}