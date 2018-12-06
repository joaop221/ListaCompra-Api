using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Base;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Produto
    /// </summary>
    [Table("Produto")]
    public class Produto : Entidade
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Nome do produtos
        /// </summary>
        [Required]
        [StringLength(150)]
        public virtual string Nome { get; set; }

        /// <summary>
        /// Descicao do prduto
        /// </summary>
        [StringLength(300)]
        public virtual string Descricao { get; set; }

        /// <summary>
        /// Quantidade do produto
        /// </summary>
        public virtual int Quantidade { get; set; }

        /// <summary>
        /// Valor do produtos(unicade)
        /// </summary>
        public virtual double Valor { get; set; }

        /// <summary>
        /// Categoria do produtos
        /// </summary>
        [ForeignKey("CategoriaId")]
        public virtual Categoria Categoria { get; set; }

        /// <summary>
        /// Id da categoria
        /// </summary>
        public virtual int CategoriaId { get; set; }

        /// <summary>
        /// Lista de listas (N para N)
        /// </summary>
        public virtual ICollection<ProdutoLista> ProdutoListas { get; set; }

    }
}