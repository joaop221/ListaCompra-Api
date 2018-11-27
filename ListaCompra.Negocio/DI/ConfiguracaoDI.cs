using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Modelo.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ListaCompra.Negocio
{
    public static class ConfiguracaoDI
    {
        public static void RepositorioDI(IServiceCollection container)
        {
            Assembly repositorioAssembly = typeof(BDContextoBase).Assembly;
            Type irepositorioBase = typeof(IRepositorio);

            //Busca toso os repositórios no assembly e as interfaces implementadas
            var registros =
                from type in repositorioAssembly.GetExportedTypes()
                where type.Namespace == "ListaCompra.Dado.Repositorios"
                where type.GetInterfaces().Any(x => x == irepositorioBase)
                where type.IsClass && !type.IsAbstract && !type.IsGenericType
                select new
                {
                    Service = type.GetInterfaces().Where(x => x != irepositorioBase), //todas as interfaces menos a base
                    Implementation = type
                };

            //Efetua o registro das implementações para resolver por
            //as requisições de (IRepositorio<T,TDbContext> e IRepositorioExemplo)
            foreach (var reg in registros)
            {
                foreach (Type service in reg.Service)
                {
                    container.AddScoped(service, reg.Implementation);
                }
            }
        }

        public static void NegocioDI(IServiceCollection servicesContainer)
        {
            IEnumerable<Type> todosTipos = typeof(ConfiguracaoDI)
             .GetTypeInfo()
             .Assembly
             .GetTypes();

            IEnumerable<Type> negociosLista = todosTipos.Where(type =>
                type.GetInterfaces().Contains(typeof(INegocio)) && !type.IsAbstract && !type.IsInterface);

            foreach (Type negocio in negociosLista)
            {
                servicesContainer.AddScoped(negocio);
            }
        }
    }
}