using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaCampoActivoEnEjecutivoTablaYLlavePrimaria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdEjecutivo",
                table: "Ejecutivos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "Ejecutivos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ejecutivos",
                table: "Ejecutivos",
                column: "IdEjecutivo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Ejecutivos",
                table: "Ejecutivos");

            migrationBuilder.DropColumn(
                name: "activo",
                table: "Ejecutivos");

            migrationBuilder.AlterColumn<int>(
                name: "IdEjecutivo",
                table: "Ejecutivos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
