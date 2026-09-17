using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaLaRelacionDeLaTablaEmpresaConLaTablaOrigenesDestinosTarifario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdEmpresa",
                table: "OrigenesDestinosTarifario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrigenesDestinosTarifario_IdEmpresa",
                table: "OrigenesDestinosTarifario",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_OrigenesDestinosTarifario_Empresa_IdEmpresa",
                table: "OrigenesDestinosTarifario",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrigenesDestinosTarifario_Empresa_IdEmpresa",
                table: "OrigenesDestinosTarifario");

            migrationBuilder.DropIndex(
                name: "IX_OrigenesDestinosTarifario_IdEmpresa",
                table: "OrigenesDestinosTarifario");

            migrationBuilder.DropColumn(
                name: "IdEmpresa",
                table: "OrigenesDestinosTarifario");
        }
    }
}
