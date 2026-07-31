using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaIconosAComercial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 176,
                column: "ruta_icono",
                value: "pi pi-bell");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 177,
                column: "ruta_icono",
                value: "pi pi-money-bill");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 204,
                column: "ruta_icono",
                value: "pi-bell");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 205,
                column: "ruta_icono",
                value: "pi pi-money-bill");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 232,
                column: "ruta_icono",
                value: "pi-bell");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 233,
                column: "ruta_icono",
                value: "pi pi-money-bill");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 260,
                column: "ruta_icono",
                value: "pi-bell");

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 261,
                column: "ruta_icono",
                value: "pi pi-money-bill");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 176,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 177,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 204,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 205,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 232,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 233,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 260,
                column: "ruta_icono",
                value: null);

            migrationBuilder.UpdateData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 261,
                column: "ruta_icono",
                value: null);
        }
    }
}
