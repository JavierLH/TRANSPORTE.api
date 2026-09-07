using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class InserccionDeNuevoPermisoEnTablaPermisos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "IdPermiso", "NombrePermiso", "padre", "ruta_icono", "ruta_modulo" },
                values: new object[] { 294, "ADMINISTRAR ROLES DE EMPRESAS", 278, null, "siil/configuraciones/administrarrolesempresas" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "IdPermiso",
                keyValue: 294);
        }
    }
}
