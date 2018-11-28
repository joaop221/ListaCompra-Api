using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Infraestrutura.Reflexao;
using ListaCompra.Modelo.Entidades;

namespace ListaCompra.Dado.EF.Core
{
    public class RepositorioBase
    {
        /// <summary>
        /// Define as propriedades básicas do objeto na operação
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="item">    Objeto</param>
        /// <param name="operacao">Operação realizada</param>
        public static void DefinirValorPadrao(Entidade item, OperacaoBanco operacao, bool validarItensFilhos = true)
        {
            if (item == null)
                return;

            var usuarioAtual = Thread.CurrentPrincipal.Identity != null && !string.IsNullOrWhiteSpace(Thread.CurrentPrincipal.Identity.Name) ?
                                 Thread.CurrentPrincipal.Identity.Name :
                                  string.Concat(Environment.UserDomainName, @"\", Environment.UserName);

            switch (operacao)
            {
                case OperacaoBanco.Inserir:
                    Reflexao.DefinePropriedade(item, CampoDB.FlagExclusao, false);
                    Reflexao.DefinePropriedade(item, CampoDB.UsuarioInclusao, usuarioAtual);
                    Reflexao.DefinePropriedade(item, CampoDB.DataInclusao, DateTime.Now);
                    break;

                case OperacaoBanco.Atualizar:
                    //Define os valores padrões
                    Reflexao.DefinePropriedade(item, CampoDB.UsuarioAlteracao, usuarioAtual);
                    Reflexao.DefinePropriedade(item, CampoDB.DataAlteracao, DateTime.Now);
                    break;

                case OperacaoBanco.ExcluirLogico:
                    //Define os valores padrões
                    Reflexao.DefinePropriedade(item, CampoDB.UsuarioAlteracao, usuarioAtual);
                    Reflexao.DefinePropriedade(item, CampoDB.DataAlteracao, DateTime.Now);
                    Reflexao.DefinePropriedade(item, CampoDB.FlagExclusao, true);
                    break;
            }

            if (!validarItensFilhos)
                return;

            //Busca todas as propriedades do item
            PropertyInfo[] propriedades = item.GetType().GetProperties();
            foreach (PropertyInfo propriedade in propriedades)
                if (propriedade.PropertyType.IsSubclassOf(typeof(Entidade)) && propriedade.GetValue(item, null) != null)
                    DefinirValorPadrao((Entidade)propriedade.GetValue(item, null), operacao, false);
                else if (
                    propriedade.PropertyType.IsGenericType &&
                    propriedade.PropertyType.GenericTypeArguments.Length > 0 &&
                    propriedade.PropertyType.GenericTypeArguments[0].IsSubclassOf(typeof(Entidade)) &&
                    propriedade.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                {
                    var valores = (IEnumerable<Entidade>)propriedade.GetValue(item, null);
                    if (valores != null)
                        foreach (Entidade valor in valores)
                            DefinirValorPadrao(valor, operacao, false);
                }
        }
    }
}