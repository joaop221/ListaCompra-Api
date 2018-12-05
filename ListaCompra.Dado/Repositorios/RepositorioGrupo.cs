using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.Repositorios
{
    /// <summary>
    /// Repositorio para checar a conxecao com o banco
    /// </summary>
    public class RepositorioGrupo : Repositorio<Grupo>, IRepositorioGrupo
    {
        private readonly IRepositorio<GrupoUsuario> repositorioGrupoUsuario;

        public RepositorioGrupo(ListaCompraBDContexto contexto, IServiceProvider services,
                                IRepositorio<GrupoUsuario> repositorioGrupoUsuario)
            : base(contexto, services)
        {
            this.repositorioGrupoUsuario = repositorioGrupoUsuario;
        }

        public async Task<List<IdentityUser>> ConsultaUsuariosDoGrupoAsync(int grupoId)
        {
            var retorno = new List<IdentityUser>();

            DbSet<IdentityUser> tabelaUsuario = this.Db.Set<IdentityUser>();

            List<GrupoUsuario> listaGrupoUsuario = await this.repositorioGrupoUsuario.ConsultarAsync(x => x.GrupoId == grupoId);
            foreach (var item in listaGrupoUsuario.Select(x => x.UsuarioId))
            {
                IdentityUser usuario = await tabelaUsuario.FindAsync(item);
                if (usuario != null)
                    retorno.Add(usuario);
            }

            return retorno;
        }
    }
}