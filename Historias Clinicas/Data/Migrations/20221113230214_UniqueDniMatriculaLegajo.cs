using Microsoft.EntityFrameworkCore.Migrations;

namespace Historias_Clinicas.Data.Migrations
{
    public partial class UniqueDniMatriculaLegajo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Personas_Legajo",
                table: "Personas",
                column: "Legajo",
                unique: true,
                filter: "[Legajo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_MatriculaNacional",
                table: "Personas",
                column: "MatriculaNacional",
                unique: true,
                filter: "[MatriculaNacional] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_Dni",
                table: "Personas",
                column: "Dni",
                unique: true,
                filter: "[Dni] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Personas_Legajo",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_MatriculaNacional",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_Dni",
                table: "Personas");
        }
    }
}
