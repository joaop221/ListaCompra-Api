using FluentValidation.AspNetCore;
using ListaCompra.Servicos.Config;
using ListaCompra.Servicos.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ListaCompra.Servicos
{
	/// <summary>
	/// Classe de inicialização da aplicação
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Configurações iniciais da aplicação
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Define connection string da aplicação
		/// </summary>
		public static string ConnectionString { get; private set; }

		/// <summary>
		/// Construtor da classe Startup
		/// </summary>
		/// <param name="configuration"></param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Método chamada pela aplicação em tempo de execução para configurar todos os serviços
		/// </summary>
		/// <param name="services">Serviços</param>
		public void ConfigureServices(IServiceCollection services)
		{
			// Configurando serviço MVC e Fluent VAlidation para validadores
			services.AddMvc(options =>
			{
				options.Filters.Add(typeof(FilterValidation));
			}).AddFluentValidation();

			// Configurando contexto do banco para EF
			services.AddDbContext<DbContext>(options =>
			{
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
			});

			// Configurando o serviço de documentação do Swagger
			services.AddSwagger();

            // Adiciona controlador de headers na aplicação
            services.AddCors();

			// Configurando serviço de autenticação
			services.AddAuth(Configuration.GetSection("App").GetValue<string>("SigningKey"));

			// Configurando serviço de cache
			services.AddSingletons();
		}

		/// <summary>
		/// Método chamada pela aplicação em tempo de execução para configurar a aplicação
		/// </summary>
		/// <param name="app">Contexto da aplicação</param>
		/// <param name="env">Ambiente</param>
		/// <param name="loggerFactory">Console de Log</param>
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			// Contexto de desenvolvimento da aplicação
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// Ativa Console de Log da aplicação
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			// Inicia serviço que permite urls consultarem API
			app.UseCors(options =>
			{
				options.WithOrigins("http://localhost:4200").AllowAnyHeader();
			});

            // Inicia serviço MVC
            app.UseMvc();

			// Inicia serviço de Auteenticação
			app.UseAuthentication();

			// Inicia serviço Swagger
			app.UseSwaggerConfig();
		}
	}
}