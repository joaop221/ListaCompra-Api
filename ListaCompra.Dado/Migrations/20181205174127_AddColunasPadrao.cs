using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaCompra.Dado.Migrations
{
    public partial class AddColunasPadrao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<DateTime>(
                name: "DataAlteracao",
                table: "GrupoUsuario",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInclusao",
                table: "GrupoUsuario",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "Excluido",
                table: "GrupoUsuario",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LoginAlteracao",
                table: "GrupoUsuario",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LoginInclusao",
                table: "GrupoUsuario",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[,]
                {
                    { "59688913-da1d-4b34-b313-cd7c115b012a", "14ba3028-10c3-4605-af97-b49871a27a28", "Admin", "ADMIN" },
                    { "ecd069dc-3094-4f8c-8717-b19f1044b402", "693c7a26-0be0-4093-8844-74072c866153", "Comum", "COMUM" },
                    { "64d38884-4240-462f-a6c6-8c123128878a", "59e65a21-14f1-4eee-8161-9ab6a93acef5", "Moderador", "MODERADOR" }
                });

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "PermissaoId",
                keyValue: 1,
                column: "DataInclusao",
                value: new DateTime(2018, 12, 5, 17, 41, 27, 394, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "PermissaoId",
                keyValue: 2,
                column: "DataInclusao",
                value: new DateTime(2018, 12, 5, 17, 41, 27, 395, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Permissao",
                keyColumn: "PermissaoId",
                keyValue: 3,
                column: "DataInclusao",
                value: new DateTime(2018, 12, 5, 17, 41, 27, 395, DateTimeKind.Utc));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "59688913-da1d-4b34-b313-cd7c115b012a", "14ba3028-10c3-4605-af97-b49871a27a28" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "64d38884-4240-462f-a6c6-8c123128878a", "59e65a21-14f1-4eee-8161-9ab6a93acef5" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "ecd069dc-3094-4f8c-8717-b19f1044b402", "693c7a26-0be0-4093-8844-74072c866153" });

            migrationBuilder.DropColumn(
                name: "DataAlteracao",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "DataInclusao",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "Excluido",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "LoginAlteracao",
                table: "GrupoUsuario");

            migrationBuilder.DropColumn(
                name: "LoginInclusao",
                table: "GrupoUsuario");

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
        }
    }
}
