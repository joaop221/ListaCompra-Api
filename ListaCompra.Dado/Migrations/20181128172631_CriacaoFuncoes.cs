using Microsoft.EntityFrameworkCore.Migrations;

namespace ListaCompra.Dado.Migrations
{
    public partial class CriacaoFuncoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[] { "94059fe4-0264-40b7-99f5-d2de1cd08625", "a3a9ecd8-8aac-4fa7-9773-a27d595547c1", "Admin", null });

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[] { "6e03a011-7f87-4ab3-b431-a4748628590d", "acf15fca-53e6-402b-b048-6874eeb66f43", "Comum", null });

            migrationBuilder.InsertData(
                table: "Funcao",
                columns: new[] { "Id", "AlteracaoStamp", "Nome", "NomeNormalizado" },
                values: new object[] { "fccd619f-390d-4d52-8490-d6a988d829c8", "0f5df287-6a12-4d05-a143-ea03e1ce940d", "Moderador", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "6e03a011-7f87-4ab3-b431-a4748628590d", "acf15fca-53e6-402b-b048-6874eeb66f43" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "94059fe4-0264-40b7-99f5-d2de1cd08625", "a3a9ecd8-8aac-4fa7-9773-a27d595547c1" });

            migrationBuilder.DeleteData(
                table: "Funcao",
                keyColumns: new[] { "Id", "AlteracaoStamp" },
                keyValues: new object[] { "fccd619f-390d-4d52-8490-d6a988d829c8", "0f5df287-6a12-4d05-a143-ea03e1ce940d" });
        }
    }
}
