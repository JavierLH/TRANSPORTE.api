using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class modificaciondetipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 3,
                column: "Descripcion",
                value: "TRACTOCAMION");

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 4,
                column: "Descripcion",
                value: "REMOLQUE");

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 5,
                column: "Descripcion",
                value: "DOLLY");

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 6,
                column: "Descripcion",
                value: "GENERADOR");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 3,
                column: "Descripcion",
                value: "TIPO DE OPERADOR");

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 4,
                column: "Descripcion",
                value: "TIPO DE CONTENEDOR");

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 5,
                column: "Descripcion",
                value: "TIPO DE MERCANCÍA");

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 6,
                column: "Descripcion",
                value: "TIPO DE PATIO");

            migrationBuilder.InsertData(
                table: "SisTipos",
                columns: new[] { "IdSisTipo", "Descripcion" },
                values: new object[,]
                {
                    { 1, "TIPO DE OPERACIÓN" },
                    { 2, "TIPO DE EQUIPO" }
                });
        }
    }
}
