using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganUnidadesKarveTR024ALTR029 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Equipos",
                columns: new[] { "IdEquipo", "Anio", "Clave", "Descripcion", "Estatus", "IdEmpresa", "IdTipoEquipo", "Marca", "Modelo", "Placa", "Serie" },
                values: new object[,]
                {
                    { 363, null, "TR024", "TR024", 1, 1, 3, null, null, null, null },
                    { 364, null, "TR025", "TR025", 1, 1, 3, null, null, null, null },
                    { 365, null, "TR026", "TR026", 1, 1, 3, null, null, null, null },
                    { 366, null, "TR027", "TR027", 1, 1, 3, null, null, null, null },
                    { 367, null, "TR028", "TR028", 1, 1, 3, null, null, null, null },
                    { 368, null, "TR029", "TR029", 1, 1, 3, null, null, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 363);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 364);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 365);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 366);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 367);

            migrationBuilder.DeleteData(
                table: "Equipos",
                keyColumn: "IdEquipo",
                keyValue: 368);
        }
    }
}
