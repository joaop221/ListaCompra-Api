using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Modelo
{
    /// <summary>
    /// Modelo de produto base que ficara salvo
    /// </summary>
    public class ProdutoBase : Base
    {
        /// <summary>
        /// Id do Produto
        /// </summary>
        public int IdProduto { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        public string Nome { get; set; }
    }
}
