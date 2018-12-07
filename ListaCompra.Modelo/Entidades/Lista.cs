using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ListaCompra.Modelo.Base;

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
        public virtual int Id { get; set; }

        /// <summary>
        /// Titulo da lista
        /// </summary>
        [Required]
        public virtual string Titulo { get; set; }

        /// <summary>
        /// Grupo da lista
        /// </summary>
        [ForeignKey("GrupoId")]
        public virtual Grupo Grupo { get; set; }

        /// <summary>
        /// Id do grupo
        /// </summary>
        public virtual int GrupoId { get; set; }

        /// <summary>
        /// Lista de produtos
        /// </summary>
        public virtual ICollection<ProdutoLista> ProdutoListas { get; set; }
    }
}