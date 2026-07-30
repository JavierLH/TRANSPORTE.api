using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class NuevosPermisosRolesMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "NombrePermiso", "padre", "ruta_modulo" },
                values: new object[,]
                {
                    { 285, "ROLES", 278, null },
                    { 286, "KARVE", 285, "siil/configuraciones/roles/karve" },
                    { 287, "DRAKKAR", 285, "siil/configuraciones/roles/drakkar" },
                    { 288, "VIKINGO", 285, "siil/configuraciones/roles/vikingo" },
                    { 289, "LETCA", 285, "siil/configuraciones/roles/letca" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 286);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 287);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 288);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 289);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 285);
        }
    }
}
