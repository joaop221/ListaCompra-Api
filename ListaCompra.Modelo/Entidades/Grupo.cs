using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Base;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Grupo
    /// </summary>
    [Table("Grupo")]
    public class Grupo : Entidade
    {
        /// <summary>
        /// Id do grupo
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Nome do Grupo
        /// </summary>
        [Required]
        [MaxLength(150)]
        public virtual string Nome { get; set; }

        /// <summary>
        /// Usuario do grupo
        /// </summary>
        public virtual ICollection<GrupoUsuario> GrupoUsuarios { get; } = new List<GrupoUsuario>();
    }
}