using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Dado.Repositorios;
using ListaCompra.Modelo;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ListaCompra.Negocio
{
    public static class ConfiguracaoDI
    {
        public static void RepositorioDI(IServiceCollection container)
        {
            Assembly modeloAssembly = typeof(RetornoErro).Assembly;

            Assembly repositorioAssembly = typeof(BDContextoBase).Assembly;
            // servico base para repositorios
            Type irepositorioBase = typeof(IRepositorio<>);
            //Implementações base para repositórios não implementados
            Type ImplementacaoBase = typeof(Repositorio<>);

            //Busca toso os repositórios no assembly e as interfaces implementadas
            var registros =
                from type in repositorioAssembly.GetExportedTypes()
                where type.Namespace == "ListaCompra.Dado.Repositorios"
                where type != ImplementacaoBase
                where type.IsClass && !type.IsAbstract && !type.IsGenericType
                select new
                {
                    Servico = type.GetInterfaces().Where(x => x != irepositorioBase), //todas as interfaces menos a base
                    Implementacao = type
                };

            //Efetua o registro das implementações para resolver por
            //as requisições de (IRepositorio<T,TDbContext> e IRepositorioExemplo)
            foreach (var reg in registros)
            {
                foreach (Type service in reg.Servico)
                {
                    container.AddScoped(service, reg.Implementacao);
                }
            }

            //Busca todas as classes da Modelo que ainda não foram registradas em repositório para serem registradas de forma genérica.
            //Isso é feito para que não seja necessário criar um repositório, caso não existam métodos customizados
            var modeloRegistro =
                from type in modeloAssembly.GetExportedTypes()
                where type.Namespace == "ListaCompra.Modelo.Entidades"
                where type.BaseType == typeof(Entidade)
                //Remove as implementações existentes de Repositórios
                where !registros.ToList().Exists(i => i.Servico.Any(s => s.GenericTypeArguments.FirstOrDefault() == type))
                select new
                {
                    Servico = irepositorioBase.MakeGenericType(type),
                    Implementacao = ImplementacaoBase.MakeGenericType(type)
                };

            // Efetua a implementação padrão de repositórios que não foram criados, por exemplo as
            // requisições de (IRepositorio<MinhaClasse> para Repositorio<MinhaClasse>)
            foreach (var reg in modeloRegistro)
                container.AddScoped(reg.Servico, reg.Implementacao);
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