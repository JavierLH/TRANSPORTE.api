using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganMenusEjecutivos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "NombrePermiso", "padre", "ruta_modulo" },
                values: new object[,]
                {
                    { 290, "EJECUTIVOS", 172, "siil/vikingo/trafico/catalogo/ejectivos" },
                    { 291, "EJECUTIVOS", 256, "siil/karve/trafico/catalogo/ejectivos" },
                    { 292, "EJECUTIVOS", 200, "siil/drakkar/trafico/catalogo/ejectivos" },
                    { 293, "EJECUTIVOS", 228, "siil/letca/trafico/catalogo/ejectivos" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 290);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 291);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 292);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 293);
        }
    }
}
