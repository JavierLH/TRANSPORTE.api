using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaCampoParaGuardarTipoDeContenedores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodigoConfiguracion",
                table: "ProgramacionViajes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProgramacionViajes_CodigoConfiguracion",
                table: "ProgramacionViajes",
                column: "CodigoConfiguracion");

            migrationBuilder.AddForeignKey(
                name: "FK_ProgramacionViajes_Configuracion_CodigoConfiguracion",
                table: "ProgramacionViajes",
                column: "CodigoConfiguracion",
                principalTable: "Configuracion",
                principalColumn: "CodigoConfiguracion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProgramacionViajes_Configuracion_CodigoConfiguracion",
                table: "ProgramacionViajes");

            migrationBuilder.DropIndex(
                name: "IX_ProgramacionViajes_CodigoConfiguracion",
                table: "ProgramacionViajes");

            migrationBuilder.DropColumn(
                name: "CodigoConfiguracion",
                table: "ProgramacionViajes");
        }
    }
}
