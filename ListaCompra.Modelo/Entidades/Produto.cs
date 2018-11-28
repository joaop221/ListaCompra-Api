using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Id { get; set; }

        /// <summary>
        /// Nome do produtos
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        /// <summary>
        /// Descicao do prduto
        /// </summary>
        [StringLength(300)]
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
        /// Categoria do produtos
        /// </summary>
        [ForeignKey("CategoriaId")]
        public Categoria Categoria { get; set; }

        /// <summary>
        /// Id da categoria
        /// </summary>
        public int CategoriaId { get; set; }

        /// <summary>
        /// Lista de listas (N para N)
        /// </summary>
        public ICollection<ProdutoLista> ProdutoListas { get; set; }

    }
}