using System;
using System.Collections.Generic;
using System.Linq;
using ListaCompra.Dado.EF.Core;
using ListaCompra.Modelo.Entidades;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            #region [ Renomeacao entidades do Identity ]

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

                entity.HasData(new IdentityRole("Admin") { NormalizedName = "ADMIN" },
                                new IdentityRole("Comum") { NormalizedName = "COMUM" },
                                new IdentityRole("Moderador") { NormalizedName = "MODERADOR" });
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

            #endregion [ Renomeacao entidades do Identity ]

            #region [ N para N ]

            builder.Entity<ProdutoLista>()
                .HasKey(bc => new { bc.ListaId, bc.ProdutoId });

            builder.Entity<ProdutoLista>()
                .HasOne(bc => bc.Lista)
                .WithMany(b => b.ProdutoListas)
                .HasForeignKey(bc => bc.ListaId);

            builder.Entity<ProdutoLista>()
                .HasOne(bc => bc.Produto)
                .WithMany(c => c.ProdutoListas)
                .HasForeignKey(bc => bc.ProdutoId);

            builder.Entity<GrupoUsuario>()
                .HasKey(bc => new { bc.UsuarioId, bc.GrupoId });

            builder.Entity<GrupoUsuario>()
                .HasOne(bc => bc.Grupo)
                .WithMany(c => c.GrupoUsuarios)
                .HasForeignKey(bc => bc.GrupoId);

            #endregion [ N para N ]

            #region [ Preenche dados ]

            builder.Entity<Permissao>()
                .HasData(new Permissao() { PermissaoId = 1, Nome = "Dono", DataInclusao = DateTime.UtcNow, LoginInclusao = "MIGRATION" },
                         new Permissao() { PermissaoId = 2, Nome = "Administrador", DataInclusao = DateTime.UtcNow, LoginInclusao = "MIGRATION" },
                         new Permissao() { PermissaoId = 3, Nome = "Contribuidor", DataInclusao = DateTime.UtcNow, LoginInclusao = "MIGRATION" });

            #endregion [ Preenche dados ]

            #region [ Delete on cascade off ]

            IEnumerable<IMutableForeignKey> cascadeFKs = builder.Model.GetEntityTypes()
                .Where(x => !x.Name.Contains("Identity"))
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (IMutableForeignKey fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            #endregion [ Delete on cascade off ]
        }

        #region [ DbSet ]

        public virtual DbSet<Categoria> Categoria { get; set; }

        public virtual DbSet<Grupo> Grupo { get; set; }

        public virtual DbSet<Lista> Lista { get; set; }

        public virtual DbSet<Produto> Produto { get; set; }

        #endregion [ DbSet ]
    }
}