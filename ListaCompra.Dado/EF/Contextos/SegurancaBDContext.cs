using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ListaCompra.Dado.EF.Contextos
{
    public class SegurancaBDContext : IdentityDbContext<IdentityUser>
    {
        public SegurancaBDContext()
        {
        }

        public SegurancaBDContext(DbContextOptions<SegurancaBDContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "Usuario");

                entity.Property(e => e.LockoutEnd).HasColumnName("FimTrava");
                entity.Property(e => e.TwoFactorEnabled).HasColumnName("DoisFatoresAtivado");
                entity.Property(e => e.PhoneNumberConfirmed).HasColumnName("NumeroTelefoneComfirmado");
                entity.Property(e => e.PhoneNumber).HasColumnName("NumeroTelefone");
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("AlteracaoStamp");
                entity.Property(e => e.SecurityStamp).HasColumnName("SegurancaStamp");
                entity.Property(e => e.PasswordHash).HasColumnName("SenhaHash");
                entity.Property(e => e.EmailConfirmed).HasColumnName("EmailConfirmado");
                entity.Property(e => e.NormalizedEmail).HasColumnName("EmailNormalizado");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.NormalizedUserName).HasColumnName("NomeUsuarioNormalizado");
                entity.Property(e => e.UserName).HasColumnName("NomeUsuario");
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.LockoutEnabled).HasColumnName("TravaAtivada");
                entity.Property(e => e.AccessFailedCount).HasColumnName("ContadorFalhaAcesso");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Funcao");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name).HasColumnName("Nome");
                entity.Property(e => e.NormalizedName).HasColumnName("NomeNormalizado");
                entity.Property(e => e.ConcurrencyStamp).HasColumnName("AlteracaoStamp");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UsuarioFuncao");

                entity.Property(e => e.RoleId).HasColumnName("IdFuncao");
                entity.Property(e => e.UserId).HasColumnName("IdUsuario");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UsuarioClaims");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.UserId).HasColumnName("IdUsuario");
                entity.Property(e => e.ClaimType).HasColumnName("TipoClaim");
                entity.Property(e => e.ClaimValue).HasColumnName("ValorClaim");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UsuarioLogins");

                entity.Property(e => e.LoginProvider).HasColumnName("ProvedorLogin");
                entity.Property(e => e.ProviderKey).HasColumnName("ChaveProvedor");
                entity.Property(e => e.ProviderDisplayName).HasColumnName("NomeExibicaoProvedor");
                entity.Property(e => e.UserId).HasColumnName("IdUsuario");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UsuariorToken");

                entity.Property(e => e.UserId).HasColumnName("IdUsuario");
                entity.Property(e => e.LoginProvider).HasColumnName("ProvedorLogin");
                entity.Property(e => e.Name).HasColumnName("Nome");
                entity.Property(e => e.Value).HasColumnName("Valor");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("FuncaoClaim");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.RoleId).HasColumnName("IdFuncao");
                entity.Property(e => e.ClaimType).HasColumnName("TipoClaim");
                entity.Property(e => e.ClaimValue).HasColumnName("ValorClaim");
            });
        }
    }
}