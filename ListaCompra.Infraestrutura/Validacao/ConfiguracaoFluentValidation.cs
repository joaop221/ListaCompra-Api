using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.AspNetCore;
using ListaCompra.Modelo;
using Microsoft.Extensions.DependencyInjection;

namespace ListaCompra.Infraestrutura.Validacao
{
    public static class ConfiguracaoFluentValidation
    {
        public static IMvcBuilder ConfigureFluentValidation(this IMvcBuilder mvcBuilder)
        {
            //Busca o assembly de modelo
            System.Reflection.Assembly modeloAssembly = typeof(RetornoErro).Assembly;

            //Busca todos os modelos no assembly que tem AbstractValidator
            var registros =
                from tipo in modeloAssembly.GetExportedTypes()
                where tipo.GetBaseTypes().Any(x => x.Name == typeof(AbstractValidator<>).Name)
                select new { Tipo = tipo };

            mvcBuilder
                .AddFluentValidation(fv =>
                {
                    foreach (var reg in registros)
                        fv.RegisterValidatorsFromAssemblyContaining(reg.Tipo);
                });

            return mvcBuilder;
        }

        private static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            if (type.BaseType == null)
                return type.GetInterfaces();

            return Enumerable.Repeat(type.BaseType, 1)
                             .Concat(type.GetInterfaces())
                             .Concat(type.GetInterfaces().SelectMany<Type, Type>(GetBaseTypes))
                             .Concat(type.BaseType.GetBaseTypes());
        }
    }
}