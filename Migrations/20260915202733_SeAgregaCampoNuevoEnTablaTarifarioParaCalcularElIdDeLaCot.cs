using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaCampoNuevoEnTablaTarifarioParaCalcularElIdDeLaCot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Folio",
                table: "Tarifario",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true,
                computedColumnSql: "('COT-' + CASE WHEN [ClaveCot] < 1000000 THEN RIGHT('000000' + CONVERT(varchar(11), [ClaveCot]), 6) ELSE CONVERT(varchar(11), [ClaveCot]) END)",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tarifario_Folio",
                table: "Tarifario",
                column: "Folio",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tarifario_Folio",
                table: "Tarifario");

            migrationBuilder.DropColumn(
                name: "Folio",
                table: "Tarifario");
        }
    }
}
