using ListaCompra.Modelo;
using ListaCompra.Modelo.Response;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ListaCompra.Servicos.Controllers
{
	/// <summary>
	/// Controller de métodos relacionados a Grupos
	/// </summary>
	[Route("api/[controller]")]
	public class GrupoController : BaseController
	{
		private NegocioGrupo negocioGrupo; 

		/// <summary>
		/// Contrutor do controller de grupo
		/// </summary>
		public GrupoController()
		{
			this.negocioGrupo = new NegocioGrupo();
		}

		/// <summary>
		/// Método padrão de criação de grupo
		/// </summary>
		/// <param name="grupo">Grupo a ser criado</param>
		/// <returns>Id do grupo criado</returns>
		[SwaggerResponse(200, Type = typeof(ItemResponse<int>))]
		[SwaggerResponse(400, Type = typeof(ItemResponse<int>))]
		[SwaggerResponse(500, Type = typeof(ItemResponse<int>))]
		[HttpPost(template: "criar")]
		public IActionResult Criar([FromBody]Grupo grupo)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				retorno.Item = this.negocioGrupo.Criar(grupo);
				retorno.CodigoRetorno = 200;
				retorno.Messagem = "Grupo criado com sucesso";
			});
		}

		/// <summary>
		/// Consulta Grupos pelo Id
		/// </summary>
		/// <returns>Grupos da consulta</returns>
		[HttpGet(template: "list")]
		public IActionResult List(int id)
		{
			return ExecucaoTratada<ListResponse<Grupo>>((retorno) =>
			{
				//retorno.Item = negocioGrupo.Consultar(id);
				retorno.Messagem = "Grupo consultada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Criação de Grupo
		/// </summary>
		/// <param name="grupo">Grupo a ser criada</param>
		/// <returns>Id do Grupo</returns>
		[HttpPost(template: "post")]
		public IActionResult Post([FromBody]Grupo grupo)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				//retorno.Item = negocioGrupo.Consultar(id);
				retorno.Messagem = "Grupo criada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Atualização de Grupo
		/// </summary>
		/// <param name="grupo">Grupo a ser atualizada</param>
		/// <returns></returns>
		[HttpPut(template: "put")]
		public IActionResult Put([FromBody]Grupo grupo)
		{
			return ExecucaoTratada<ItemResponse<int>>((retorno) =>
			{
				//retorno.Item = negocioGrupo.Consultar(id);
				retorno.Messagem = "Grupo atualizada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}

		/// <summary>
		/// Deleta Grupo pelo Id
		/// </summary>
		/// <param name="id">Id da Grupo a ser deletada</param>
		/// <returns>True se sucesso</returns>
		[HttpDelete(template: "delete")]
		public IActionResult Delete(int id)
		{
			return ExecucaoTratada<ItemResponse<bool>>((retorno) =>
			{
				//retorno.Item = negocioGrupo.Consultar(id);
				retorno.Messagem = "Grupo deletada com sucesso";
				retorno.CodigoRetorno = 200;
			});
		}
	}
}