using System.Threading.Tasks;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Modelo.Entidades;
using ListaCompra.Modelo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.Repositorios
{
    /// <summary>
    /// Repositorio para checar a conxecao com o banco
    /// </summary>
    public class RepositorioSaude : Repositorio<Entidade>, IRepositorioSaude
    {
        public RepositorioSaude(ListaCompraBDContexto contexto) : base(contexto)
        { }

        public async Task Check()
            => await this.Db.Database.ExecuteSqlCommandAsync("SELECT 1");
    }
}