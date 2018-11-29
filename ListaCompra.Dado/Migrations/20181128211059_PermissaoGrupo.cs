using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaCompra.Dado.Migrations
{
    public partial class PermissaoGrupo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "303be0ff-15c8-484b-a9de-6970ce05f583", "9dbb7b53-98cb-47d0-8255-211de3e0ad25" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "933ea249-7afb-4fd3-842e-0265b26e8dde", "41270b2b-4e6b-4a0c-9ec1-14d2712abe87" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "dfe406bf-6977-463e-a3bb-34be16cd68d4", "c9eae67c-06cc-4e63-b075-28a994c3ae82" });

            migrationBuilder.AddColumn<string>(
                name: "PermissaoId",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermissaoId1",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Permissao",
                columns: table => new
                {
                    LoginInclusao = table.Column<string>(maxLength: 200, nullable: false),
                    DataInclusao = table.Column<DateTime>(nullable: false),
                    LoginAlteracao = table.Column<string>(maxLength: 200, nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Excluido = table.Column<bool>(nullable: false),
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissao", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[,]
                {
                    { "4ed6f594-9e9f-4c9a-8fb2-e00cdbea4983", "0207a901-ac46-430f-89ea-99972de8a48d", "Admin", "ADMIN" },
                    { "587690cf-96a3-4c08-ac2b-54e7a0be85e5", "51db3c94-eb94-4343-8d07-039b44b593dd", "Comum", "COMUM" },
                    { "c47ae563-ad71-4ee8-8352-dc442300c654", "05fe7d17-72aa-4b38-97b2-8ca7b132c25f", "Moderador", "MODERADOR" }
                });

            migrationBuilder.InsertData(
                table: "Permissao",
                columns: new[] { "Id", "DataAlteracao", "DataInclusao", "Excluido", "LoginAlteracao", "LoginInclusao", "Nome" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2018, 11, 28, 21, 10, 59, 5, DateTimeKind.Utc), false, null, "MIGRATION", "Dono" },
                    { 2, null, new DateTime(2018, 11, 28, 21, 10, 59, 5, DateTimeKind.Utc), false, null, "MIGRATION", "Administrador" },
                    { 3, null, new DateTime(2018, 11, 28, 21, 10, 59, 5, DateTimeKind.Utc), false, null, "MIGRATION", "Contribuidor" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_PermissaoId1",
                table: "GrupoUsuario",
                column: "PermissaoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoUsuario_Permissao_PermissaoId1",
                table: "GrupoUsuario",
                column: "PermissaoId1",
                principalTable: "Permissao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoUsuario_Permissao_PermissaoId1",
                table: "GrupoUsuario");

            migrationBuilder.DropTable(
                name: "Permissao");

            migrationBuilder.DropIndex(
                name: "IX_GrupoUsuario_PermissaoId1",
                table: "GrupoUsuario");

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "4ed6f594-9e9f-4c9a-8fb2-e00cdbea4983", "0207a901-ac46-430f-89ea-99972de8a48d" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "587690cf-96a3-4c08-ac2b-54e7a0be85e5", "51db3c94-eb94-4343-8d07-039b44b593dd" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "c47ae563-ad71-4ee8-8352-dc442300c654", "05fe7d17-72aa-4b38-97b2-8ca7b132c25f" });

            migrationBuilder.DropColumn(
                name: "PermissaoId",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "PermissaoId1",
                table: "GrupoUsuario");

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
        }
    }
}
