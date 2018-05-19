using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace ListaCompra.Servicos
{
	/// <summary>
	/// Classe de inicialização da aplicação
	/// </summary>
    public class Startup
    {
		/// <summary>
		/// Construtor da classe Startup
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Configurações iniciais da aplicação
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Método chamada pela aplicação em tempo de execução para configurar todos os serviços
		/// </summary>
		/// <param name="services">Serviços</param>
		public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

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
		/// Método chamada pela aplicação em tempo de execução para configurar a aplicação
		/// </summary>
		/// <param name="app">Contexto da aplicação</param>
		/// <param name="env">Ambiente</param>
		/// <param name="loggerFactory">Log</param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseMvc();

			// Ativando middlewares para uso do Swagger
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/ui/index",
					"Lista de Compra - API"); 
			});
		}
    }
}
