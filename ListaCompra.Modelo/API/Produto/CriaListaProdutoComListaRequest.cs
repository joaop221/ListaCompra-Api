using System.Collections.Generic;

namespace ListaCompra.Modelo.API.Produto
{
    /// <summary>
    /// Usado para criar varios produtos e atrelar eles à uma lista
    /// </summary>
    public class CriaListaProdutoComListaRequest
    {
        /// <summary>
        /// Id da Lista
        /// </summary>
        public int ListaId { get; set; }

        /// <summary>
        /// Produto
        /// </summary>
        public List<ProdutoRequest> Produtos { get; set; }
    }
}