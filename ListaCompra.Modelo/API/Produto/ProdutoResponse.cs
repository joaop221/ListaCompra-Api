using System;
using System.Collections.Generic;
using System.Text;

namespace ListaCompra.Modelo.API.Produto
{
    /// <summary>
    /// Response de produtos
    /// </summary>
    public class ProdutoResponse
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nome do produtos
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Descicao do prduto
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Quantidade do produto
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Valor do produtos(unicade)
        /// </summary>
        public double Valor { get; set; }

        /// <summary>
        /// Id da categoria
        /// </summary>
        public int CategoriaId { get; set; }
    }
}
