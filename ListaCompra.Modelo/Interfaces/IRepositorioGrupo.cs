using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Modelo.Entidades;
using Microsoft.AspNetCore.Identity;

namespace ListaCompra.Modelo.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepositorioGrupo : IRepositorio<Grupo>
    {
        /// <summary>
        /// Consula os usuario de um grupo
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns></returns>
        Task<List<IdentityUser>> ConsultaUsuariosDoGrupoAsync(int grupoId);
    }
}
