using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class OperacionImportacionExportacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdOperacion",
                table: "Operacion",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Operacion",
                table: "Operacion",
                column: "IdOperacion");

            migrationBuilder.InsertData(
                table: "Operacion",
                columns: new[] { "IdOperacion", "TipoOperacion" },
                values: new object[,]
                {
                    { 1, "IMPORTACION" },
                    { 2, "EXPORTACION" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Operacion",
                table: "Operacion");

            migrationBuilder.DeleteData(
                table: "Operacion",
                keyColumn: "IdOperacion",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Operacion",
                keyColumn: "IdOperacion",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "IdOperacion",
                table: "Operacion",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
