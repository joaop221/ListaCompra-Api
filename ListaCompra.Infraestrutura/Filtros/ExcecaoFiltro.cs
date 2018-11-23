using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using ListaCompra.Infraestrutura.Tratamento;
using ListaCompra.Modelo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ListaCompra.Infraestrutura.Filtros
{
    public class ExcecaoFiltro : ExceptionFilterAttribute
    {
        private readonly ILogger<ExcecaoFiltro> _logger;

        public ExcecaoFiltro(ILogger<ExcecaoFiltro> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// Trata Exception que não foram previamente tratadas.
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            Exception ex = context.Exception;

            // Usa a primeira Exception caso ela seja uma AggregateException
            if (ex is AggregateException aggregate)
                ex = aggregate.InnerException;

            context.Result = CriaResponse(ex);
        }

        private ContentResult CriaResponse(Exception ex)
        {
            if (ex is ValidacaoExcecao excecaoValidacao) // Em caso de erro de validacao, usado pelo metodo 'Validate' do BaseController
            {
                this._logger.LogWarning(excecaoValidacao, excecaoValidacao.Message);

                List<RetornoErro> erros = excecaoValidacao.ValidacaoErros;
                return CriaResponse(erros, HttpStatusCode.BadRequest);
            }
            else if (ex is ApiExcecao excecaoApi) // Em caso de erros que já foram tratados e devem ser lidados como um HttpStatus
            {
                this._logger.LogError(excecaoApi, excecaoApi.Message);

                List<RetornoErro> erros = CriaListaErro(excecaoApi.Codigo, GetExceptionFont(excecaoApi), excecaoApi.Message);
                return CriaResponse(erros, excecaoApi.StatusCode);
            }
            else if (ex is NegocioExcecao excecaoNegocio) // Em caso de erros na camada de negocio
            {
                this._logger.LogError(excecaoNegocio, excecaoNegocio.Message);

                List<RetornoErro> erros = CriaListaErro(500, GetExceptionFont(excecaoNegocio), excecaoNegocio.Message);
                return CriaResponse(erros);
            }
            else if (ex is DadosExcecao excecaoDados) // Em caso de erros na camada de dados
            {
                this._logger.LogError(excecaoDados, excecaoDados.Message);

                List<RetornoErro> erros = CriaListaErro(500, GetExceptionFont(excecaoDados), excecaoDados.Detalhes);
                return CriaResponse(erros);
            }
            else // Em caso de erros genericos
            {
                var excecaoSistema = ex as Exception;
                this._logger.LogError(excecaoSistema, excecaoSistema.Message);

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

            return query.First().Class.FullName;
        }
    }
}