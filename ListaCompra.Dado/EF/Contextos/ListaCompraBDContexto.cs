using ListaCompra.Dado.EF.Core;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.EF.Contextos
{
    public class ListaCompraBDContexto : BDContextoBase
    {
        public ListaCompraBDContexto()
        {
        }

        public ListaCompraBDContexto(DbContextOptions<ListaCompraBDContexto> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // chama método base
            base.OnModelCreating(modelBuilder);
        }

        //Criar os DbSets aqui
    }
}