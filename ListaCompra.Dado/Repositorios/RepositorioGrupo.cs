using System;
using System.Linq;
using System.Threading.Tasks;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Modelo.Base;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.Repositorios
{
    /// <summary>
    /// Repositorio para checar a conxecao com o banco
    /// </summary>
    public class RepositorioGrupo : Repositorio<Grupo>, IRepositorioGrupo
    {

        public RepositorioGrupo(ListaCompraBDContexto contexto, IServiceProvider services,
                                IRepositorio<GrupoUsuario> repositorioGrupoUsuario)
            : base(contexto, services)
        {
        }

        /// <summary>
        /// Consulta Grupos com os usuario
        /// </summary>
        /// <param name="grupoId"></param>
        /// <returns></returns>
        public async Task<Grupo> ConsultaGrupoComUsuariosAsync(int grupoId)
        {
            Grupo resultado = await this.Db.Set<Grupo>()
                                        .Where(x => x.Id == grupoId)
                                        .Include(grupo => grupo.GrupoUsuarios)
                                            .ThenInclude(t => t.Usuario)
                                        .FirstOrDefaultAsync();

            return resultado;
        }
    }
}