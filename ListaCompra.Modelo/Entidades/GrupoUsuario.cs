using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Relacionamento entre grupo e usuario
    /// </summary>
    [Table("GrupoUsuario")]
    public class GrupoUsuario
    {
        /// <summary>
        /// Id da Grupo x Usuario
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Id da Grupo
        /// </summary>
        public int GrupoId { get; set; }

        /// <summary>
        /// Grupo
        /// </summary>
        [ForeignKey("GrupoId")]
        public Grupo Grupo { get; set; }

        /// <summary>
        /// Id do Usuarios
        /// </summary>
        public string UsuarioId { get; set; }

        /// <summary>
        /// Usuarios
        /// </summary>
        [ForeignKey("UsuarioId")]
        public IdentityUser Usuario { get; set; }

        /// <summary>
        /// Id da Permissao
        /// </summary>
        [ForeignKey("PermissaoId")]
        public int PermissaoId { get; set; }

        /// <summary>
        /// Permissao
        /// </summary>
        public Permissao Permissao { get; set; }
    }
}