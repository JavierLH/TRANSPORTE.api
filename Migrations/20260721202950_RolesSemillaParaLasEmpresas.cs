using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class RolesSemillaParaLasEmpresas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "IdRol", "Descripcion", "IdEmpresa", "NombreRol" },
                values: new object[,]
                {
                    { 7, "Coordinador de Tráfico Karve", 1, "COORDINADOR DE TRAFICO" },
                    { 8, "Ejecutivo Karve", 1, "EJECUTIVO" },
                    { 9, "Patio Karve", 1, "PATIO" },
                    { 10, "Jefe de Tráfico Karve", 1, "JEFE DE TRAFICO" },
                    { 11, "Coordinador de Tráfico Vikingo", 2, "COORDINADOR DE TRAFICO" },
                    { 12, "Ejecutivo Vikingo", 2, "EJECUTIVO" },
                    { 13, "Patio Vikingo", 2, "PATIO" },
                    { 14, "Jefe de Tráfico Vikingo", 2, "JEFE DE TRAFICO" },
                    { 15, "Coordinador de Tráfico Drakkar", 3, "COORDINADOR DE TRAFICO" },
                    { 16, "Ejecutivo Drakkar", 3, "EJECUTIVO" },
                    { 17, "Patio Drakkar", 3, "PATIO" },
                    { 18, "Jefe de Tráfico Drakkar", 3, "JEFE DE TRAFICO" },
                    { 19, "Coordinador de Tráfico Letca", 4, "COORDINADOR DE TRAFICO" },
                    { 20, "Ejecutivo Letca", 4, "EJECUTIVO" },
                    { 21, "Patio Letca", 4, "PATIO" },
                    { 22, "Jefe de Tráfico Letca", 4, "JEFE DE TRAFICO" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "IdRol",
                keyValue: 22);
        }
    }
}
