using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaPrefijoDeLaEmpresaEnLaTablaEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PrefijoDocumentos",
                table: "Empresa",
                type: "varchar(5)",
                unicode: false,
                maxLength: 5,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 1,
                column: "PrefijoDocumentos",
                value: "KV");

            migrationBuilder.UpdateData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 2,
                column: "PrefijoDocumentos",
                value: "VV");

            migrationBuilder.UpdateData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 3,
                column: "PrefijoDocumentos",
                value: "DK");

            migrationBuilder.UpdateData(
                table: "Empresa",
                keyColumn: "IdEmpresa",
                keyValue: 4,
                column: "PrefijoDocumentos",
                value: "LTC");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrefijoDocumentos",
                table: "Empresa");
        }
    }
}
