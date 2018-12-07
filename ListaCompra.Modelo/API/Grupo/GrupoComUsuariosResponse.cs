using System.Collections.Generic;
using ListaCompra.Modelo.API.Conta;

namespace ListaCompra.Modelo.API.Grupo
{
    /// <summary>
    /// Grupo Com Usuarios Response
    /// </summary>
    public class GrupoComUsuariosResponse
    {
        /// <summary>
        /// Grupo
        /// </summary>
        public GrupoResponse Grupo { get; set; }

        /// <summary>
        /// Lista de usuarios
        /// </summary>
        public List<Usuario> Usuarios { get; set; }
    }
}
