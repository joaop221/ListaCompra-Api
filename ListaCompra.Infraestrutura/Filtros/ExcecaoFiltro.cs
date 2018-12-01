using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using ListaCompra.Infraestrutura.Tratamento;
using ListaCompra.Modelo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Serilog.Context;

namespace ListaCompra.Infraestrutura.Filtros
{
    public class ExcecaoFiltro : ExceptionFilterAttribute
    {
        private readonly ILogger<ExcecaoFiltro> logger;
        private UserManager<IdentityUser> userManager;
        private readonly IServiceProvider service;

        public ExcecaoFiltro(ILogger<ExcecaoFiltro> logger, IServiceProvider service)
        {
            this.logger = logger;
            this.service = service;
        }

        /// <summary>
        /// Trata Exception que não foram previamente tratadas.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;

            var userId = RecuperaUsuarioID(context.HttpContext.User);

            LogContext.PushProperty("UsuarioId", userId);

            // Usa a primeira Exception caso ela seja uma AggregateException
            if (ex is AggregateException aggregate)
                ex = aggregate.InnerException;

            context.Result = CriaResponse(ex);
        }

        private string RecuperaUsuarioID(ClaimsPrincipal claims)
        {
            if (this.userManager == null)
                this.userManager = (UserManager<IdentityUser>)this.service.GetService(typeof(UserManager<IdentityUser>));

            var userId = this.userManager?.GetUserId(claims);
            return userId;
        }

        private ContentResult CriaResponse(Exception ex)
        {
            if (ex is ValidacaoExcecao excecaoValidacao) // Em caso de erro de validacao, usado pelo metodo 'Validate' do BaseController
            {
                this.logger.LogWarning(excecaoValidacao, excecaoValidacao.Message);

                List<RetornoErro> erros = excecaoValidacao.ValidacaoErros;
                return CriaResponse(erros, HttpStatusCode.BadRequest);
            }
            else if (ex is ApiExcecao excecaoApi) // Em caso de erros que já foram tratados e devem ser lidados como um HttpStatus
            {
                this.logger.LogError(excecaoApi, excecaoApi.Message);

                List<RetornoErro> erros = CriaListaErro(excecaoApi.Codigo, GetExceptionFont(excecaoApi), excecaoApi.Message);
                return CriaResponse(erros, excecaoApi.StatusCode);
            }
            else if (ex is NegocioExcecao excecaoNegocio) // Em caso de erros na camada de negocio
            {
                this.logger.LogError(excecaoNegocio, excecaoNegocio.Message);

                List<RetornoErro> erros = CriaListaErro(500, GetExceptionFont(excecaoNegocio), excecaoNegocio.Message);
                return CriaResponse(erros);
            }
            else if (ex is DadosExcecao excecaoDados) // Em caso de erros na camada de dados
            {
                this.logger.LogError(excecaoDados, excecaoDados.Message);

                List<RetornoErro> erros = CriaListaErro(500, GetExceptionFont(excecaoDados), excecaoDados.Detalhes);
                return CriaResponse(erros);
            }
            else if (ex is FalhaLoginExcecao falhaLogin) // Em caso de erros na camada de dados
            {
                this.logger.LogWarning(falhaLogin, falhaLogin.Message);

                List<RetornoErro> erros = CriaListaErro(401, GetExceptionFont(falhaLogin), falhaLogin.Message);
                return CriaResponse(erros, HttpStatusCode.Unauthorized);
            }
            else if (ex is RegistroExcecao registroExcecao) // Em caso de erros na camada de dados
            {
                this.logger.LogWarning(registroExcecao, registroExcecao.Message);

                return new ContentResult()
                {
                    ContentType = "application/json",
                    StatusCode = 422,
                    Content = JsonConvert.SerializeObject(registroExcecao.Erros)
                };
            }
            else // Em caso de erros genericos
            {
                var excecaoSistema = ex as Exception;
                this.logger.LogError(excecaoSistema, excecaoSistema.Message);

                List<RetornoErro> erros = CriaListaErro(500, GetExceptionFont(excecaoSistema), excecaoSistema.Message);
                return CriaResponse(erros);
            }
        }

        private ContentResult CriaResponse(List<RetornoErro> erros, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new ContentResult()
            {
                ContentType = "application/json",
                StatusCode = (int)statusCode,
                Content = JsonConvert.SerializeObject(erros)
            };
        }

        //
        private List<RetornoErro> CriaListaErro(int codigo, string fonte, string msg)
        {
            return new List<RetornoErro>()
                    {
                        new RetornoErro()
                        {
                            CodigoRetorno = codigo,
                            FonteErro = fonte,
                            MensagemRetorno = msg
                        }
                    };
        }

        /// <summary>
        /// Recupera o arquivo de origem da exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public string GetExceptionFont(Exception ex)
        {
            var st = new StackTrace(ex, true);
            var query = st.GetFrames()
                          .Select(frame => new
                          {
                              FileName = frame.GetFileName(),
                              LineNumber = frame.GetFileLineNumber(),
                              ColumnNumber = frame.GetFileColumnNumber(),
                              Method = frame.GetMethod(),
                              Class = frame.GetMethod().DeclaringType,
                          });

            if (query.FirstOrDefault() == null)
                return ex.StackTrace;

            return query.FirstOrDefault().Class.FullName;
        }
    }
}