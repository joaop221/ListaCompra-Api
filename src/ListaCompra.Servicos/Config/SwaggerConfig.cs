using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace ListaCompra.Servicos.Config
{
	/// <summary>
	/// Classe de configuração e especificação dos serviços do Swagger
	/// </summary>
	public static class SwaggerConfig
	{
		/// <summary>
		/// Adiciona e configura serviços do Swagger
		/// </summary>
		/// <param name="services">serviços da aplicação</param>
		public static void AddSwagger(this IServiceCollection services)
		{
			// Configurando o serviço de documentação do Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new Info
				{
					Title = "Lista de Compra - API",
					Version = "v1",
					Description = "API REST criada com o ASP.NET Core de Lista de Compra",
					Contact = new Contact
					{
						Name = "João Pedro",
						Url = "https://github.com/joaop221"
					}
				});

				// Define o caminho dos arquivos de documentação do Swagger
				string xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
				string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		/// <summary>
		/// Ativa serviços do swagger na aplicação e define caminho do json
		/// </summary>
		/// <param name="app">Contexto da aplicação</param>
		public static void UseSwaggerConfig(this IApplicationBuilder app)
		{
			// Ativando middlewares para uso do Swagger
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json",
					"Lista de Compra - API");
			});
		}
	}
}