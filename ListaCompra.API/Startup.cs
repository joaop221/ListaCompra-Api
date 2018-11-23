using System;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Infraestrutura.Filtros;
using ListaCompra.Infraestrutura.Swagger;
using ListaCompra.Infraestrutura.Validacao;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace ListaCompra.API
{
    public class Startup
    {
        private readonly ILogger<ExcecaoFiltro> _loggerEX;

        public Startup(IConfiguration configuration, ILogger<ExcecaoFiltro> logEx)
        {
            this.Configuration = configuration;
            this._loggerEX = logEx;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(opcoes => AddFilters(opcoes))
                .ConfigureFluentValidation()
                .AddJsonOptions(opcoes => AddJsonOptions(opcoes))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Configura swagger ui
            services
                .ConfiguraSwaggerGen();

            //Ação de configuração para ser reutilizada
            Action<DbContextOptionsBuilder> dbOptionsAction =
                (options) =>
                {
                    options.UseSqlServer(this.Configuration.GetConnectionString("Default"));
                };

            //Configura o contexto principal
            services.AddDbContext<ListaCompraBDContexto>(dbOptionsAction);

            // Injeta todos os repositorios
            ConfiguracaoDI.RepositorioDI(services);
            // Injeta todas as classes de negocio
            ConfiguracaoDI.NegocioDI(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Adicionar o Serilog
            loggerFactory.AddSerilog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseMvc()
                .ConfiguraSwaggerUI();
        }

        private void AddFilters(MvcOptions opcoes)
        {
            opcoes.Filters.Add(new ValidacaoFiltro());
            opcoes.Filters.Add(new ExcecaoFiltro(this._loggerEX));
        }

        private void AddJsonOptions(MvcJsonOptions opcoes)
        {
            //ignore nulls no json
            opcoes.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            //Usa camel case no json
            opcoes.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //não identa o Json (ocupa menos espaço)
            opcoes.SerializerSettings.Formatting = Formatting.None;
        }
    }
}