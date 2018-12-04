using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaCompra.Dado.Migrations
{
    public partial class PermissaoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoUsuario_Permissao_PermissaoId1",
                table: "GrupoUsuario");

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
                name: "PermissaoId1",
                table: "GrupoUsuario");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Permissao",
                newName: "PermissaoId");

            migrationBuilder.RenameColumn(
                name: "GrupoId",
                table: "Grupo",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "PermissaoId",
                table: "GrupoUsuario",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Grupo",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[,]
                {
                    { "a9350483-529a-40a9-b135-e1dcee70946d", "f599b1f2-d676-456a-b7a5-70e9c00cffb0", "Admin", "ADMIN" },
                    { "bfc0b258-070d-4c00-bf08-814443582f75", "51087dd1-4859-4732-ac7c-f36ef26d54f7", "Comum", "COMUM" },
                    { "4cb57189-60de-4712-a527-ba0d345800dc", "949974ea-a6e0-4a99-86cc-2a1efa676700", "Moderador", "MODERADOR" }
                });

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "PermissaoId",
                keyValue: 1,
                column: "DataInclusao",
                value: new DateTime(2018, 12, 3, 22, 6, 22, 80, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "PermissaoId",
                keyValue: 2,
                column: "DataInclusao",
                value: new DateTime(2018, 12, 3, 22, 6, 22, 81, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "PermissaoId",
                keyValue: 3,
                column: "DataInclusao",
                value: new DateTime(2018, 12, 3, 22, 6, 22, 81, DateTimeKind.Utc));

            migrationBuilder.CreateIndex(
                name: "IX_GrupoUsuario_PermissaoId",
                table: "GrupoUsuario",
                column: "PermissaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_GrupoUsuario_Permissao_PermissaoId",
                table: "GrupoUsuario",
                column: "PermissaoId",
                principalTable: "Permissao",
                principalColumn: "PermissaoId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GrupoUsuario_Permissao_PermissaoId",
                table: "GrupoUsuario");

            migrationBuilder.DropIndex(
                name: "IX_GrupoUsuario_PermissaoId",
                table: "GrupoUsuario");

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "4cb57189-60de-4712-a527-ba0d345800dc", "949974ea-a6e0-4a99-86cc-2a1efa676700" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "a9350483-529a-40a9-b135-e1dcee70946d", "f599b1f2-d676-456a-b7a5-70e9c00cffb0" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "bfc0b258-070d-4c00-bf08-814443582f75", "51087dd1-4859-4732-ac7c-f36ef26d54f7" });

            migrationBuilder.RenameColumn(
                name: "PermissaoId",
                table: "Permissao",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Grupo",
                newName: "GrupoId");

            migrationBuilder.AlterColumn<string>(
                name: "PermissaoId",
                table: "GrupoUsuario",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PermissaoId1",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Grupo",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[,]
                {
                    { "4ed6f594-9e9f-4c9a-8fb2-e00cdbea4983", "0207a901-ac46-430f-89ea-99972de8a48d", "Admin", "ADMIN" },
                    { "587690cf-96a3-4c08-ac2b-54e7a0be85e5", "51db3c94-eb94-4343-8d07-039b44b593dd", "Comum", "COMUM" },
                    { "c47ae563-ad71-4ee8-8352-dc442300c654", "05fe7d17-72aa-4b38-97b2-8ca7b132c25f", "Moderador", "MODERADOR" }
                });

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 1,
                column: "DataInclusao",
                value: new DateTime(2018, 11, 28, 21, 10, 59, 5, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 2,
                column: "DataInclusao",
                value: new DateTime(2018, 11, 28, 21, 10, 59, 5, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataInclusao",
                value: new DateTime(2018, 11, 28, 21, 10, 59, 5, DateTimeKind.Utc));

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
    }
}
