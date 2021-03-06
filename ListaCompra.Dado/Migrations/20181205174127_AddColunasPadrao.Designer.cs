﻿// <auto-generated />
using System;
using ListaCompra.Dado.EF.Contextos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ListaCompra.Dado.Migrations
{
    [DbContext(typeof(ListaCompraBDContexto))]
    [Migration("20181205174127_AddColunasPadrao")]
    partial class AddColunasPadrao
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("Descricao")
                        .HasMaxLength(300);

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Grupo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Grupo");
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.GrupoUsuario", b =>
                {
                    b.Property<string>("UsuarioId");

                    b.Property<int>("GrupoId");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.Property<int>("PermissaoId");

                    b.HasKey("UsuarioId", "GrupoId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("GrupoId");

                    b.HasIndex("PermissaoId");

                    b.ToTable("GrupoUsuario");
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Lista", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<int>("GrupoId");

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.Property<string>("Titulo")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("GrupoId");

                    b.ToTable("Lista");
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Permissao", b =>
                {
                    b.Property<int>("PermissaoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("PermissaoId");

                    b.ToTable("Permissao");

                    b.HasData(
                        new { PermissaoId = 1, DataInclusao = new DateTime(2018, 12, 5, 17, 41, 27, 394, DateTimeKind.Utc), Excluido = false, LoginInclusao = "MIGRATION", Nome = "Dono" },
                        new { PermissaoId = 2, DataInclusao = new DateTime(2018, 12, 5, 17, 41, 27, 395, DateTimeKind.Utc), Excluido = false, LoginInclusao = "MIGRATION", Nome = "Administrador" },
                        new { PermissaoId = 3, DataInclusao = new DateTime(2018, 12, 5, 17, 41, 27, 395, DateTimeKind.Utc), Excluido = false, LoginInclusao = "MIGRATION", Nome = "Contribuidor" }
                    );
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoriaId");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<string>("Descricao")
                        .HasMaxLength(300);

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("Quantidade");

                    b.Property<double>("Valor");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Produto");
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.ProdutoLista", b =>
                {
                    b.Property<int>("ListaId");

                    b.Property<int>("ProdutoId");

                    b.Property<DateTime?>("DataAlteracao")
                        .HasColumnName("DataAlteracao");

                    b.Property<DateTime>("DataInclusao")
                        .HasColumnName("DataInclusao");

                    b.Property<bool>("Excluido")
                        .HasColumnName("Excluido");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LoginAlteracao")
                        .HasColumnName("LoginAlteracao")
                        .HasMaxLength(200);

                    b.Property<string>("LoginInclusao")
                        .IsRequired()
                        .HasColumnName("LoginInclusao")
                        .HasMaxLength(200);

                    b.HasKey("ListaId", "ProdutoId");

                    b.HasAlternateKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("ProdutoLista");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("AlteracaoStamp");

                    b.Property<string>("Name")
                        .HasColumnName("Nome")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnName("NomeNormalizado")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NomeNormalizado] IS NOT NULL");

                    b.ToTable("Funcao");

                    b.HasData(
                        new { Id = "59688913-da1d-4b34-b313-cd7c115b012a", ConcurrencyStamp = "14ba3028-10c3-4605-af97-b49871a27a28", Name = "Admin", NormalizedName = "ADMIN" },
                        new { Id = "ecd069dc-3094-4f8c-8717-b19f1044b402", ConcurrencyStamp = "693c7a26-0be0-4093-8844-74072c866153", Name = "Comum", NormalizedName = "COMUM" },
                        new { Id = "64d38884-4240-462f-a6c6-8c123128878a", ConcurrencyStamp = "59e65a21-14f1-4eee-8161-9ab6a93acef5", Name = "Moderador", NormalizedName = "MODERADOR" }
                    );
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnName("TipoClaim");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("ValorClaim");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnName("IdFuncao");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("FuncaoClaim");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnName("ContadorFalhaAcesso");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnName("AlteracaoStamp");

                    b.Property<string>("Email")
                        .HasColumnName("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnName("EmailConfirmado");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnName("TravaAtivada");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnName("FimTrava");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnName("EmailNormalizado")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnName("NomeUsuarioNormalizado")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnName("SenhaHash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnName("NumeroTelefone");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnName("NumeroTelefoneComfirmado");

                    b.Property<string>("SecurityStamp")
                        .HasColumnName("SegurancaStamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnName("DoisFatoresAtivado");

                    b.Property<string>("UserName")
                        .HasColumnName("NomeUsuario")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NomeUsuarioNormalizado] IS NOT NULL");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnName("TipoClaim");

                    b.Property<string>("ClaimValue")
                        .HasColumnName("ValorClaim");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("IdUsuario");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsuarioClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnName("ProvedorLogin");

                    b.Property<string>("ProviderKey")
                        .HasColumnName("ChaveProvedor");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnName("NomeExibicaoProvedor");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnName("IdUsuario");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UsuarioLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("IdUsuario");

                    b.Property<string>("RoleId")
                        .HasColumnName("IdFuncao");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UsuarioFuncao");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnName("IdUsuario");

                    b.Property<string>("LoginProvider")
                        .HasColumnName("ProvedorLogin");

                    b.Property<string>("Name")
                        .HasColumnName("Nome");

                    b.Property<string>("Value")
                        .HasColumnName("Valor");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UsuariorToken");
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.GrupoUsuario", b =>
                {
                    b.HasOne("ListaCompra.Modelo.Entidades.Grupo", "Grupo")
                        .WithMany("GrupoUsuarios")
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ListaCompra.Modelo.Entidades.Permissao", "Permissao")
                        .WithMany()
                        .HasForeignKey("PermissaoId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "Usuario")
                        .WithMany()
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Lista", b =>
                {
                    b.HasOne("ListaCompra.Modelo.Entidades.Grupo", "Grupo")
                        .WithMany()
                        .HasForeignKey("GrupoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.Produto", b =>
                {
                    b.HasOne("ListaCompra.Modelo.Entidades.Categoria", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("ListaCompra.Modelo.Entidades.ProdutoLista", b =>
                {
                    b.HasOne("ListaCompra.Modelo.Entidades.Lista", "Lista")
                        .WithMany("ProdutoListas")
                        .HasForeignKey("ListaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("ListaCompra.Modelo.Entidades.Produto", "Produto")
                        .WithMany("ProdutoListas")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
