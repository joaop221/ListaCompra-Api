using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ListaCompra.Infraestrutura.Swagger
{
    /// <summary>
    /// </summary>
    public static class ConfiguracaoSwagger
    {
        public static IServiceCollection ConfiguraSwaggerGen(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1",
                        new Info
                        {
                            Title = "Our List API",
                            Version = "v1",
                            License = new License()
                            {
                                Name = string.Format("© Copyright {0} OurHome", DateTime.Now.Year),
                            }
                        });

                    c.EnableAnnotations();

                    c.IncludeXmlComments(string.Format(@"{0}\ListaCompra.Modelo.xml", AppDomain.CurrentDomain.BaseDirectory), true);
                    c.IncludeXmlComments(string.Format(@"{0}\ListaCompra.API.xml", AppDomain.CurrentDomain.BaseDirectory), true);

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "Header de Autorização JWT usando o esquema Bearer. Exemplo: \"Autorização: Bearer { token } \"", Name = "Authorization", Type = "apiKey" });
                    c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                        { "Bearer", Enumerable.Empty<string>() }
                    });
                });

            return services;
        }

        public static void ConfiguraSwaggerUI(this IApplicationBuilder app)
        {
            app
                .UseSwagger()
                .UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "OurListV1");
                    c.DisplayRequestDuration();
                })
                .UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));
        }
    }
}