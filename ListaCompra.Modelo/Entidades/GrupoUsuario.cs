using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using ListaCompra.Modelo.Base;

namespace ListaCompra.Modelo.Entidades
{
    /// <summary>
    /// Relacionamento entre grupo e usuario
    /// </summary>
    [Table("GrupoUsuario")]
    public class GrupoUsuario : Entidade
    {

        /// <summary>
        /// Cria relacionamento do grupo e usuario
        /// </summary>
        /// <param name="grupoId"></param>
        /// <param name="usuarioId"></param>
        /// <param name="permissaoId"></param>
        public GrupoUsuario(int grupoId, string usuarioId, int permissaoId)
        {
            this.GrupoId = grupoId;
            this.UsuarioId = usuarioId;
            this.PermissaoId = permissaoId;
        }

        /// <summary>
        /// Cria relacionamento do grupo e usuario
        /// </summary>
        public GrupoUsuario()
        {
        }

        /// <summary>
        /// Id da Grupo x Usuario
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        /// <summary>
        /// Id da Grupo
        /// </summary>
        public virtual int GrupoId { get; set; }

        /// <summary>
        /// Grupo
        /// </summary>
        [ForeignKey("GrupoId")]
        public virtual Grupo Grupo { get; set; }

        /// <summary>
        /// Id do Usuarios
        /// </summary>
        public virtual string UsuarioId { get; set; }

        /// <summary>
        /// Usuarios
        /// </summary>
        [ForeignKey("UsuarioId")]
        public virtual IdentityUser Usuario { get; set; }

        /// <summary>
        /// Id da Permissao
        /// </summary>
        [ForeignKey("PermissaoId")]
        public virtual int PermissaoId { get; set; }

        /// <summary>
        /// Permissao
        /// </summary>
        public virtual Permissao Permissao { get; set; }
    }
}