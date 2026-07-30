using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganMenusEjecutivosSeArreglanRutasMalEscritas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 290,
                column: "ruta_modulo",
                value: "siil/vikingo/trafico/catalogo/ejecutivos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 291,
                column: "ruta_modulo",
                value: "siil/karve/trafico/catalogo/ejecutivos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 292,
                column: "ruta_modulo",
                value: "siil/drakkar/trafico/catalogo/ejecutivos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 293,
                column: "ruta_modulo",
                value: "siil/letca/trafico/catalogo/ejecutivos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 290,
                column: "ruta_modulo",
                value: "siil/vikingo/trafico/catalogo/ejectivos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 291,
                column: "ruta_modulo",
                value: "siil/karve/trafico/catalogo/ejectivos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 292,
                column: "ruta_modulo",
                value: "siil/drakkar/trafico/catalogo/ejectivos");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 293,
                column: "ruta_modulo",
                value: "siil/letca/trafico/catalogo/ejectivos");
        }
    }
}
