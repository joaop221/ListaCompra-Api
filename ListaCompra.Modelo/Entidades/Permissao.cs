using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Base;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Permissao
    /// </summary>
    [Table("Permissao")]
    public class Permissao : Entidade
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int PermissaoId { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        [MaxLength(50)]
        public virtual string Nome { get; set; }
    }
}