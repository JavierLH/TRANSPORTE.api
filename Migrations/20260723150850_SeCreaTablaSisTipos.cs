using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeCreaTablaSisTipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SisTipos",
                columns: table => new
                {
                    IdSisTipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SisTipos", x => x.IdSisTipo);
                });

            migrationBuilder.InsertData(
                table: "SisTipos",
                columns: new[] { "IdSisTipo", "Descripcion" },
                values: new object[,]
                {
                    { 1, "TIPO DE OPERACIÓN" },
                    { 2, "TIPO DE EQUIPO" },
                    { 3, "TIPO DE OPERADOR" },
                    { 4, "TIPO DE CONTENEDOR" },
                    { 5, "TIPO DE MERCANCÍA" },
                    { 6, "TIPO DE PATIO" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipos_IdTipoEquipo",
                table: "Equipos",
                column: "IdTipoEquipo");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipos_SisTipos_IdTipoEquipo",
                table: "Equipos",
                column: "IdTipoEquipo",
                principalTable: "SisTipos",
                principalColumn: "IdSisTipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipos_SisTipos_IdTipoEquipo",
                table: "Equipos");

            migrationBuilder.DropTable(
                name: "SisTipos");

            migrationBuilder.DropIndex(
                name: "IX_Equipos_IdTipoEquipo",
                table: "Equipos");
        }
    }
}
