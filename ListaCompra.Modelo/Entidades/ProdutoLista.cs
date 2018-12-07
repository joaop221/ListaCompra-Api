using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Base;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Relacionamento entre lista e produtos
    /// </summary>
    [Table("ProdutoLista")]
    public class ProdutoLista : Entidade
    {
        /// <summary>
        /// Contrutor padrão
        /// </summary>
        public ProdutoLista()
        {
        }

        /// <summary>
        /// Contrutor que inicia os IDs
        /// </summary>
        /// <param name="produtoId"></param>
        /// <param name="listaId"></param>
        public ProdutoLista(int produtoId, int listaId)
        {
            this.ProdutoId = produtoId;
            this.ListaId = listaId;
        }

        /// <summary>
        /// Id da lista x produto
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Id da lista
        /// </summary>
        public virtual int ListaId { get; set; }

        /// <summary>
        /// Lista
        /// </summary>
        [ForeignKey("ListaId")]
        public virtual Lista Lista { get; set; }

        /// <summary>
        /// Id do produtos
        /// </summary>
        public virtual int ProdutoId { get; set; }

        /// <summary>
        /// Produtos
        /// </summary>
        [ForeignKey("ProdutoId")]
        public virtual Produto Produto { get; set; }
    }
}