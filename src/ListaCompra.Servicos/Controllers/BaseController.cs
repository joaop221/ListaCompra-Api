using ListaCompra.Modelo.Response;
using ListaCompra.Modelo.Tratamento;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ListaCompra.Servicos.Controllers
{
	/// <summary>
	/// Controller base da aplicação
	/// </summary>
	public abstract class BaseController : Controller
	{
		/// <summary>
		/// Construtor da classe
		/// </summary>
		public BaseController()
		{
		}

        /// <summary>
        /// Usuario que esta demandando a ação
        /// </summary>
        protected virtual int IdUsuario { get; set; }

		/// <summary>
		/// Execução tratada
		/// </summary>
		/// <typeparam name="TResult">Tipo do Resultado da chamada</typeparam>
		/// <param name="action">Ação da Execução</param>
		/// <returns>Resultado da chamada</returns>
		protected IActionResult ExecucaoTratada<TResult>(Action<TResult> action)
			where TResult : BaseResponse, new()
		{
			TResult retorno = new TResult();

			if (VerificaAuth())
			{

				try
				{
					action.Invoke(retorno);
				}
				catch (NegocioExcacao ex)
				{
					retorno.CodigoRetorno = ex.Codigo;
					retorno.Messagem = ex.Mensagem;
				}
				catch (DadosExcecao ex)
				{
					retorno.CodigoRetorno = ex.Codigo;
					retorno.Messagem = ex.Mensagem;
				}
				catch (Exception ex)
				{
					retorno.CodigoRetorno = 500;
					retorno.Messagem = ex.Message;
					retorno.Excecao = ex;
				}
			}
			else
			{
				retorno.CodigoRetorno = 401;
				retorno.Messagem = "Acesso negado";
			}

			return StatusCode(retorno.CodigoRetorno, retorno);
		}

		/// <summary>
		/// Verifica token de autorização
		/// </summary>
		/// <returns>True se for valido</returns>
		private bool VerificaAuth()
		{
			return true;
		}
	}
}