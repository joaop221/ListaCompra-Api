using System.Collections.Generic;
using System.Linq;
using System.Net;
using ListaCompra.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ListaCompra.Infraestrutura.Filtros
{
    public class ValidacaoFiltro : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var listErros = new List<RetornoErro>();
                foreach (Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry valor in context.ModelState.Values)
                {
                    valor.Errors.ToList().ForEach(error =>
                    {
                        if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                        {
                            listErros.Add(new RetornoErro(error.ErrorMessage, 400));
                        }
                    });
                }

                if (listErros.Count > 0)
                {
                    context.Result = new ContentResult()
                    {
                        ContentType = "application/json",
                        StatusCode = (int)HttpStatusCode.BadRequest,
                        Content = JsonConvert.SerializeObject(listErros)
                    };
                }
            }
        }
    }
}