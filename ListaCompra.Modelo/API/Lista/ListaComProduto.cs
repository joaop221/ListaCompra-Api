using System.Collections.Generic;
using ListaCompra.Modelo.API.Grupo;
using ListaCompra.Modelo.API.Produto;

namespace ListaCompra.Modelo.API.Lista
{
    /// <summary>
    /// Response de lista
    /// </summary>
    public class ListaComProduto
    {
        /// <summary>
        /// Id da lista
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titulo da lista
        /// </summary>
        public string Titulo { get; set; }

        /// <summary>
        /// Grupo
        /// </summary>
        public GrupoResponse Grupo { get; set; }

        /// <summary>
        /// Lista de produtos
        /// </summary>
        public List<ProdutoResponse> Produtos { get; set; }
    }
}