using System;
using System.Collections.Generic;
using System.Text;
using ListaCompra.Modelo.API.Conta;

namespace ListaCompra.Modelo.API.Grupo
{
    /// <summary>
    /// Request para criar um grupo com usuarios
    /// </summary>
    public class GrupoComUsuariosRequest
    {
        /// <summary>
        /// Grupo
        /// </summary>
        public GrupoRequest Grupo { get; set; }

        /// <summary>
        /// Lista de Usuarios
        /// </summary>
        public List<Usuario> Usuarios { get; set; }
    }
}
