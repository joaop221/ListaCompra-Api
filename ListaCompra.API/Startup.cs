using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Infraestrutura.Filtros;
using ListaCompra.Infraestrutura.Seguranca;
using ListaCompra.Infraestrutura.Swagger;
using ListaCompra.Infraestrutura.Validacao;
using ListaCompra.Negocio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace ListaCompra.API
{
    public class Startup
    {
        private readonly ILogger<ExcecaoFiltro> loggerEX;
        private readonly IServiceProvider service;
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration, ILogger<ExcecaoFiltro> logEx, IServiceProvider service)
        {
            this.configuration = configuration;
            this.loggerEX = logEx;
            this.service = service;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper();

            // desabilita resposta de bad request automaticas
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            // Configura swagger ui
            services
                .ConfiguraSwaggerGen();

            // Configura conexao com o banco de dados
            ConfiguraBanco(services);

            // Configura autenticacao e autorizacao com jwt
            ConfiguraSeguranca(services);

            // Injeta todos os repositorios
            ConfiguracaoDI.RepositorioDI(services);
            // Injeta todas as classes de negocio
            ConfiguracaoDI.NegocioDI(services);

            services
                .AddMvc(opcoes => AddFilters(opcoes))
                .ConfigureFluentValidation()
                .AddJsonOptions(opcoes => AddJsonOptions(opcoes))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                .UseAuthentication()
                .UseMvc()
                .ConfiguraSwaggerUI();
        }

        private void AddFilters(MvcOptions opcoes)
        {
            var userManager = (UserManager<IdentityUser>)this.service.GetService(typeof(UserManager<IdentityUser>));
            opcoes.Filters.Add(new ValidacaoFiltro());
            opcoes.Filters.Add(new ExcecaoFiltro(this.loggerEX, this.service));
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

        //local function de configuração para ser reutilizada
        private void DbOptionsActionDefault(DbContextOptionsBuilder options)
            => options.UseSqlServer(this.configuration.GetConnectionString("Default"));

        private void ConfiguraBanco(IServiceCollection services)
        {
            // Configura o contexto principal
            services.AddDbContext<ListaCompraBDContexto>(DbOptionsActionDefault);

            // Injeta o contexto padrão
            services.AddScoped(typeof(BDContextoBase), typeof(ListaCompraBDContexto));
        }

        private void ConfiguraSeguranca(IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ListaCompraBDContexto>()
            .AddDefaultTokenProviders()
            .AddErrorDescriber<DescricaoCustomizadaIdentityError>();

            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = this.configuration["JwtIssuer"],
                        ValidAudience = this.configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["ChaveJwt"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
        }
    }
}