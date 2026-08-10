using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganEquiposLetcaEnLaTablaEquipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Equipos",
                columns: new[] { "IdEquipo", "Anio", "Clave", "Descripcion", "Estatus", "IdEmpresa", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[,]
                {
                    { 400, null, "L001", "L001", 1, 4, 3, null, null, null, null },
                    { 401, null, "L002", "L002", 1, 4, 3, null, null, null, null },
                    { 402, null, "L003", "L003", 1, 4, 3, null, null, null, null },
                    { 403, null, "L004", "L004", 1, 4, 3, null, null, null, null },
                    { 404, null, "L005", "L005", 1, 4, 3, null, null, null, null },
                    { 405, null, "L006", "L006", 1, 4, 3, null, null, null, null },
                    { 406, null, "L007", "L007", 1, 4, 3, null, null, null, null },
                    { 407, null, "L008", "L008", 1, 4, 3, null, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 400);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 401);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 402);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 403);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 404);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 405);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 406);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 407);
        }
    }
}
