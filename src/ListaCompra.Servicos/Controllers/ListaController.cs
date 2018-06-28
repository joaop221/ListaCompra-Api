using ListaCompra.Modelo;
using ListaCompra.Modelo.Response;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;

namespace ListaCompra.Servicos.Controllers
{
	/// <summary>
	/// Controller de métodos relacionados a Lista de Produtos
	/// </summary>
	[Route("api/[controller]")]
	public class ListaController : BaseController
	{
        private NegocioLista negocioLista;

        /// <summary>
        /// Contrutor do controller de listas
        /// </summary>
        public ListaController()
        {
            this.negocioLista = new NegocioLista();
        }

		/// <summary>
		/// Consulta lista pelo Id
		/// </summary>
		/// <returns>Lista da consulta</returns>
		[HttpGet(template: "get")]
		public IActionResult Get(int id)
		{
            return ExecucaoTratada<ItemResponse<Lista>>((retorno) =>
            {
                retorno.Item = negocioLista.Consultar(id);
                retorno.Messagem = "Lista consultada com sucesso";
                retorno.CodigoRetorno = 200;
            });
		}

		/// <summary>
		/// Criação de Lista
		/// </summary>
		/// <param name="lista">Lista a ser criada</param>
		/// <returns>Id da lista</returns>
		[HttpPost(template: "post")]
		public IActionResult Post([FromBody]Lista lista)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				//retorno.Item = negocioLista.Consultar(id);
				retorno.Messagem = "Lista criada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Atualização de Lista
		/// </summary>
		/// <param name="lista">Lista a ser atualizada</param>
		/// <returns></returns>
		[HttpPut(template: "put")]
		public IActionResult Put([FromBody]Lista lista)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				//retorno.Item = negocioLista.Consultar(id);
				retorno.Messagem = "Lista atualizada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Deleta Lista pelo Id
		/// </summary>
		/// <param name="id">Id da Lista a ser deletada</param>
		/// <returns>True se sucesso</returns>
		[HttpDelete(template: "delete")]
		public IActionResult Delete(int id)
		{
			return ExecucaoTratada<ItemResponse<bool>>((retorno) =>
			{
				//retorno.Item = negocioLista.Consultar(id);
				retorno.Messagem = "Lista deletada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}
	}
}