using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeModificaPermisoRutaKarveYVikingo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 280,
                column: "ruta_modulo",
                value: "siil/configuraciones/permisos/karve");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 282,
                column: "ruta_modulo",
                value: "siil/configuraciones/permisos/vikingo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 280,
                column: "ruta_modulo",
                value: "siil/configuraciones/permisos/vikingo");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 282,
                column: "ruta_modulo",
                value: "siil/configuraciones/permisos/karve");
        }
    }
}
