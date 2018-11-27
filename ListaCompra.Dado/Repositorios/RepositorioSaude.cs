using System.Threading.Tasks;
using ListaCompra.Dado.EF.Contextos;
using ListaCompra.Modelo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.Repositorios
{
    public class RepositorioSaude : IRepositorioSaude
    {
        private readonly ListaCompraBDContexto contexto;

        public RepositorioSaude(ListaCompraBDContexto contexto)
        {
            this.contexto = contexto;
        }

        public async Task Check()
            => await this.contexto.Database.ExecuteSqlCommandAsync("SELECT 1");
    }
}