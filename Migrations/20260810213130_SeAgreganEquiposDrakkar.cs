using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganEquiposDrakkar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SisTipos",
                columns: new[] { "IdSisTipo", "Descripcion", "Estatus" },
                values: new object[] { 7, "RABON", 1 });

            migrationBuilder.InsertData(
                table: "Equipos",
                columns: new[] { "IdEquipo", "Anio", "Clave", "Descripcion", "Estatus", "IdEmpresa", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[,]
                {
                    { 500, null, "20", "20", 1, 3, 7, null, null, null, null },
                    { 501, null, "21", "21", 1, 3, 7, null, null, null, null },
                    { 502, null, "23", "23", 1, 3, 7, null, null, null, null },
                    { 503, null, "24", "24", 1, 3, 7, null, null, null, null },
                    { 504, null, "25", "25", 1, 3, 7, null, null, null, null },
                    { 505, null, "26", "26", 1, 3, 7, null, null, null, null },
                    { 506, null, "30", "30", 1, 3, 7, null, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 500);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 501);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 502);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 503);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 504);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 505);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 506);

            migrationBuilder.DeleteData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 7);
        }
    }
}
