using System;
using System.Reflection;

namespace ListaCompra.Infraestrutura.Reflexao
{
    /// <summary>
    /// Métodos para reflexão de código
    /// </summary>
    public static class Reflexao
    {
        public static string DllFullName() => Assembly.GetExecutingAssembly().FullName;

        /// <summary>
        /// Efetua a cópia de objetos
        /// </summary>
        /// <typeparam name="T">Tipo do objeto a ser copiado</typeparam>
        /// <param name="objetoFonte">  Origem</param>
        /// <param name="objetoDestino">Destino</param>
        public static void CopiarObjeto<T>(T objetoFonte, T objetoDestino)
        {
            foreach (PropertyInfo property in typeof(T).GetProperties())
                if (property.PropertyType.Namespace == "System"
                    || property.PropertyType.Namespace == "System.Xml.Linq")
                {
                    if (property.CanWrite)
                        property.SetValue(objetoDestino, property.GetValue(objetoFonte, null), null);
                }
        }

        /// <summary>
        /// Verifica se existe a propriedade no objeto
        /// </summary>
        /// <param name="objeto_">     Objeto</param>
        /// <param name="propriedade_">Propriedade</param>
        /// <returns>Se existe</returns>
        public static void DefinePropriedade(object objeto_, string propriedade_, object valor_)
        {
            Type myType = objeto_.GetType();

            //Busca os métodos
            PropertyInfo p = myType.GetProperty(propriedade_);

            if (p != null)
                p.SetValue(objeto_, valor_, null);
        }

        /// <summary>
        /// Retorna o valor da propriedade
        /// </summary>
        /// <param name="objeto_">     Objeto</param>
        /// <param name="propriedade_">Propriedade</param>
        /// <returns>Valor</returns>
        public static object BuscarValorPropriedade(object objeto_, string propriedade_)
        {
            Type myType = objeto_.GetType();
            //Busca os métodos
            PropertyInfo p = myType.GetProperty(propriedade_);
            if (p != null)
                return p.GetValue(objeto_, null);
            else
                return null;
        }

        /// <summary>
        /// Retorna o valor da propriedade
        /// </summary>
        /// <param name="objeto_">     Objeto</param>
        /// <param name="propriedade_">Propriedade</param>
        /// <returns>Valor</returns>
        public static object BuscarValorPropriedade(Type tipo_, string propriedade_)
        {
            //Busca os métodos
            PropertyInfo p = tipo_.GetProperty(propriedade_, BindingFlags.Static);
            if (p != null)
                return p.GetValue(null, null);
            else
                return null;
        }
    }
}