using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ListaCompra.Servicos.Filters
{
	/// <summary>
	/// Filtra e valida requisições
	/// </summary>
	public class FilterValidation : ActionFilterAttribute
	{
		/// <summary>
		/// Valida entrada
		/// </summary>
		/// <param name="actionContext"></param>
		public override void OnActionExecuting(ActionExecutingContext actionContext)
		{
			if (!actionContext.ModelState.IsValid)
			{
				actionContext.Result = new BadRequestObjectResult(actionContext.ModelState);
			}
		}
	}
}