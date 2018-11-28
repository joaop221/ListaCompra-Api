using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaCompra.Dado.Migrations
{
    public partial class CriacaoInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    LoginInclusao = table.Column<string>(maxLength: 200, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    LoginAlteracao = table.Column<string>(maxLength: 200, nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcao",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(maxLength: 256, nullable: true),
                    NomeNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    AlteracaoStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grupo",
                columns: table => new
                {
                    LoginInclusao = table.Column<string>(maxLength: 200, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    LoginAlteracao = table.Column<string>(maxLength: 200, nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo", x => x.GrupoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    NomeUsuario = table.Column<string>(maxLength: 256, nullable: true),
                    NomeUsuarioNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmado = table.Column<bool>(nullable: false),
                    SenhaHash = table.Column<string>(nullable: true),
                    SegurancaStamp = table.Column<string>(nullable: true),
                    AlteracaoStamp = table.Column<string>(nullable: true),
                    NumeroTelefone = table.Column<string>(nullable: true),
                    NumeroTelefoneComfirmado = table.Column<bool>(nullable: false),
                    DoisFatoresAtivado = table.Column<bool>(nullable: false),
                    FimTrava = table.Column<DateTimeOffset>(nullable: true),
                    TravaAtivada = table.Column<bool>(nullable: false),
                    ContadorFalhaAcesso = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    LoginInclusao = table.Column<string>(maxLength: 200, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    LoginAlteracao = table.Column<string>(maxLength: 200, nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(maxLength: 300, nullable: true),
                    Quantidade = table.Column<int>(nullable: false),
                    Valor = table.Column<double>(nullable: false),
                    CategoriaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FuncaoClaim",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdFuncao = table.Column<string>(nullable: false),
                    TipoClaim = table.Column<string>(nullable: true),
                    ValorClaim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FuncaoClaim", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FuncaoClaim_Funcao_IdFuncao",
                        column: x => x.IdFuncao,
                        principalTable: "Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lista",
                columns: table => new
                {
                    LoginInclusao = table.Column<string>(maxLength: 200, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    LoginAlteracao = table.Column<string>(maxLength: 200, nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lista", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lista_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "GrupoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrupoUsuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrupoId = table.Column<int>(nullable: false),
                    UsuarioId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrupoUsuario", x => new { x.UsuarioId, x.GrupoId });
                    table.UniqueConstraint("AK_GrupoUsuario_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Grupo_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupo",
                        principalColumn: "GrupoId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GrupoUsuario_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdUsuario = table.Column<string>(nullable: false),
                    TipoClaim = table.Column<string>(nullable: true),
                    ValorClaim = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioClaims_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioFuncao",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(nullable: false),
                    IdFuncao = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioFuncao", x => new { x.IdUsuario, x.IdFuncao });
                    table.ForeignKey(
                        name: "FK_UsuarioFuncao_Funcao_IdFuncao",
                        column: x => x.IdFuncao,
                        principalTable: "Funcao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioFuncao_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioLogins",
                columns: table => new
                {
                    ProvedorLogin = table.Column<string>(nullable: false),
                    ChaveProvedor = table.Column<string>(nullable: false),
                    NomeExibicaoProvedor = table.Column<string>(nullable: true),
                    IdUsuario = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioLogins", x => new { x.ProvedorLogin, x.ChaveProvedor });
                    table.ForeignKey(
                        name: "FK_UsuarioLogins_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariorToken",
                columns: table => new
                {
                    IdUsuario = table.Column<string>(nullable: false),
                    ProvedorLogin = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariorToken", x => new { x.IdUsuario, x.ProvedorLogin, x.Nome });
                    table.ForeignKey(
                        name: "FK_UsuariorToken_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProdutoLista",
                columns: table => new
                {
                    LoginInclusao = table.Column<string>(maxLength: 200, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    LoginAlteracao = table.Column<string>(maxLength: 200, nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ListaId = table.Column<int>(nullable: false),
                    ProdutoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutoLista", x => new { x.ListaId, x.ProdutoId });
                    table.UniqueConstraint("AK_ProdutoLista_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProdutoLista_Lista_ListaId",
                        column: x => x.ListaId,
                        principalTable: "Lista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProdutoLista_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[] { "933ea249-7afb-4fd3-842e-0265b26e8dde", "41270b2b-4e6b-4a0c-9ec1-14d2712abe87", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[] { "dfe406bf-6977-463e-a3bb-34be16cd68d4", "c9eae67c-06cc-4e63-b075-28a994c3ae82", "Comum", "COMUM" });

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[] { "303be0ff-15c8-484b-a9de-6970ce05f583", "9dbb7b53-98cb-47d0-8255-211de3e0ad25", "Moderador", "MODERADOR" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Funcao",
                column: "NomeNormalizado",
                unique: true,
                filter: "[NomeNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FuncaoClaim_IdFuncao",
                table: "FuncaoClaim",
                column: "IdFuncao");

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_GrupoId",
                table: "GrupoUsuario",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Lista_GrupoId",
                table: "Lista",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProdutoLista_ProdutoId",
                table: "ProdutoLista",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuario",
                column: "EmailNormalizado");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuario",
                column: "NomeUsuarioNormalizado",
                unique: true,
                filter: "[NomeUsuarioNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioClaims_IdUsuario",
                table: "UsuarioClaims",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioFuncao_IdFuncao",
                table: "UsuarioFuncao",
                column: "IdFuncao");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioLogins_IdUsuario",
                table: "UsuarioLogins",
                column: "IdUsuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FuncaoClaim");

            migrationBuilder.DropTable(
                name: "GrupoUsuario");

            migrationBuilder.DropTable(
                name: "ProdutoLista");

            migrationBuilder.DropTable(
                name: "UsuarioClaims");

            migrationBuilder.DropTable(
                name: "UsuarioFuncao");

            migrationBuilder.DropTable(
                name: "UsuarioLogins");

            migrationBuilder.DropTable(
                name: "UsuariorToken");

            migrationBuilder.DropTable(
                name: "Lista");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Funcao");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Grupo");

            migrationBuilder.DropTable(
                name: "Categoria");
        }
    }
}
