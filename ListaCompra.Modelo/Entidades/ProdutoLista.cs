using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Relacionamento entre lista e produtos
    /// </summary>
    [Table("ProdutoLista")]
    public class ProdutoLista : Entidade
    {
        /// <summary>
        /// Id da lista x produto
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id da lista
        /// </summary>
        public int ListaId { get; set; }

        /// <summary>
        /// Lista
        /// </summary>
        [ForeignKey("ListaId")]
        public Lista Lista { get; set; }

        /// <summary>
        /// Id do produtos
        /// </summary>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Produtos
        /// </summary>
        [ForeignKey("ProdutoId")]
        public Produto Produto { get; set; }
    }
}