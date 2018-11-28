using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Lista
    /// </summary>
    [Table("Lista")]
    public class Lista : Entidade
    {
        /// <summary>
        /// Id da lista
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Titulo da lista
        /// </summary>
        [Required]
        public string Titulo { get; set; }

        /// <summary>
        /// Grupo da lista
        /// </summary>
        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }

        /// <summary>
        /// Id do grupo
        /// </summary>
        public int GrupoId { get; set; }

        /// <summary>
        /// Lista de produtos
        /// </summary>
        public ICollection<ProdutoLista> ProdutoListas { get; set; }
    }
}