using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Base;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Categoria
    /// </summary>
    [Table("Categoria")]
    public class Categoria : Entidade
    {
        /// <summary>
        /// ID da categoria
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Nome da categoria
        /// </summary>
        [StringLength(100)]
        [Required]
        public virtual string Nome { get; set; }

        /// <summary>
        /// Decricao da categoria
        /// </summary>
        [StringLength(300)]
        public virtual string Descricao { get; set; }
    }
}