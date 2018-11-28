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
        public int GrupoId { get; set; }

        /// <summary>
        /// Nome do Grupo
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Usuario do grupo
        /// </summary>
        public ICollection<GrupoUsuario> GrupoUsuarios { get; set; }
    }
}