using System.Collections.Generic;
using System.Linq;
using System.Net;
using ListaCompra.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                foreach (ModelStateEntry valor in context.ModelState.Values)
                {
                    valor.Errors.ToList().ForEach(error =>
                    {
                        if (!string.IsNullOrWhiteSpace(error.ErrorMessage))
                        {
                            listErros.Add(new RetornoErro(error.ErrorMessage, 400));
                        }
                    });
                }

                if (listErros.Any())
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

    public class ModelStateValidatorConvension : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (ControllerModel controllerModel in application.Controllers)
            {
                controllerModel.Filters.Add(new ValidacaoFiltro());
            }
        }
    }
}