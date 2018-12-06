using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.EF.Core
{
    public class BDContextoBase : IdentityDbContext<IdentityUser>
    {
        public bool ExecutandoTransacao { get; set; }

        public BDContextoBase() : base()
        {
            //Desligado o LazyLoading do EF não permitindo carregar dados filhos após a carga da entidade
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public BDContextoBase(DbContextOptions option) : base(option)
        {
            //Desligado o LazyLoading do EF não permitindo carregar dados filhos após a carga da entidade
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public override void Dispose()
        {
            if (!this.ExecutandoTransacao)
                base.Dispose();
        }
    }
}