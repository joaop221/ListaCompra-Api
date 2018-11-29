using System;
using System.Collections.Generic;
using System.Globalization;
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

            mvcBuilder
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssembly(modeloAssembly);
                });

            ValidatorOptions.LanguageManager.Culture = new CultureInfo("pt-BR");

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