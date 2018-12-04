using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int Id { get; set; }

        /// <summary>
        /// Nome do Grupo
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        /// <summary>
        /// Usuario do grupo
        /// </summary>
        public ICollection<GrupoUsuario> GrupoUsuarios { get; } = new List<GrupoUsuario>();
    }
}