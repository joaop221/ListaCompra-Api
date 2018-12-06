using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListaCompra.Infraestrutura.Util
{
    public static class ListaHelper
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
            => items.GroupBy(property).Select(x => x.First());
    }
}
