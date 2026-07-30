using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEstatusSisTipos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estatus",
                table: "SisTipos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 3,
                column: "Estatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 4,
                column: "Estatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 5,
                column: "Estatus",
                value: 1);

            migrationBuilder.UpdateData(
                table: "SisTipos",
                keyColumn: "IdSisTipo",
                keyValue: 6,
                column: "Estatus",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estatus",
                table: "SisTipos");
        }
    }
}
