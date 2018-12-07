using System.Collections.Generic;
using System.Threading.Tasks;
using ListaCompra.Modelo.Base;
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
        /// Consulta Grupos com os usuario
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns></returns>
        Task<Grupo> ConsultaGrupoComUsuariosAsync(int grupoId);
    }
}
