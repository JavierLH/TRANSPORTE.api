using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class seAgregaTipoPatioYRelacionEntreTipoPatioYPatio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operacion",
                table: "Patios");

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Patios");

            migrationBuilder.RenameTable(
                name: "Patios",
                newName: "Patio");

            migrationBuilder.AlterColumn<int>(
                name: "IdPatio",
                table: "Patio",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoOperacion",
                table: "Patio",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdTipoPatio",
                table: "Patio",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patio",
                table: "Patio",
                column: "IdPatio");

            migrationBuilder.CreateTable(
                name: "TipoPatio",
                columns: table => new
                {
                    idTipoPatio = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPatio", x => x.idTipoPatio);
                });

            migrationBuilder.InsertData(
                table: "Patio",
                columns: new[] { "IdPatio", "Descripcion", "Estatus", "IdEstado", "IdTipoOperacion", "IdTipoPatio" },
                values: new object[,]
                {
                    { 1, "RICSA", 1, 28, 1, 19 },
                    { 2, "ICAVE T2", 1, 28, 1, 19 },
                    { 3, "CICE", 1, 28, 1, 19 },
                    { 4, "GOLMEX", 1, 28, 1, 19 },
                    { 5, "CIF", 1, 28, 1, 19 },
                    { 6, "ICAVE T2", 1, 28, 2, 20 },
                    { 7, "CICE M7", 1, 28, 2, 20 },
                    { 8, "CICE TMU", 1, 28, 2, 20 },
                    { 9, "ICAVE ZAL FREPOOL", 1, 28, null, 21 },
                    { 10, "CCI ZAL", 1, 28, null, 21 },
                    { 11, "GOLMEX AGM", 1, 28, null, 21 },
                    { 12, "GOLMEX 13.5", 1, 28, null, 21 },
                    { 13, "CCIS", 1, 28, null, 21 },
                    { 14, "ISL", 1, 28, null, 21 },
                    { 15, "TMM", 1, 28, null, 21 },
                    { 16, "TERRAPORTS", 1, 28, null, 21 },
                    { 17, "CONTAINER PARK", 1, 28, null, 21 },
                    { 18, "CIMA", 1, 28, null, 21 },
                    { 19, "CONTENEDORES DEL GOLFO", 1, 28, null, 21 },
                    { 20, "ICAVE SANTA FE", 1, 28, null, 21 },
                    { 21, "CICE SAN JULIAN", 1, 28, null, 21 },
                    { 22, "SEABORD", 1, 28, null, 21 },
                    { 23, "TEAYL", 1, 28, null, 21 },
                    { 24, "CONTAINER WASH", 1, 28, null, 21 },
                    { 25, "GARCA", 1, 28, null, 21 },
                    { 26, "SSA PATIO EXTERNO 2", 1, 7, null, 21 },
                    { 27, "NO APLICA", 1, null, null, null },
                    { 28, "SSA PATIO EXTERNO 1", 1, 7, null, 21 },
                    { 29, "IMPALA", 1, 7, null, 21 },
                    { 30, "HADRON", 1, 7, null, 21 },
                    { 31, "JACCER", 1, 7, null, 21 },
                    { 32, "CIMA", 1, 7, null, 21 },
                    { 33, "CIMA TEC 2 ASIPONA ISL", 1, 7, null, 21 },
                    { 34, "NALIGOVI", 1, 7, null, 21 },
                    { 35, "MCT", 1, 7, null, 21 },
                    { 36, "CASA", 1, 7, null, 21 },
                    { 37, "ACOMAN", 1, 7, null, 21 },
                    { 38, "SLTC", 1, 7, null, 21 },
                    { 39, "ALCAM PACIFICO", 1, 7, null, 21 },
                    { 40, "ALMAN", 1, 7, null, 21 },
                    { 41, "HAZESA", 1, 7, null, 21 },
                    { 42, "SICE GROUP", 1, 7, null, 21 },
                    { 43, "OCUPA", 1, 7, 1, 19 },
                    { 44, "CONTECON", 1, 7, 1, 19 },
                    { 45, "TIMSA", 1, 7, 1, 19 },
                    { 46, "FRIMAN", 1, 7, 1, 19 },
                    { 47, "SSA", 1, 7, 1, 19 }
                });

            migrationBuilder.InsertData(
                table: "TipoPatio",
                columns: new[] { "idTipoPatio", "descripcion", "estatus" },
                values: new object[,]
                {
                    { 19, "RETIROS", 1 },
                    { 20, "INGRESOS", 1 },
                    { 21, "VACIOS", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoPatio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patio",
                table: "Patio");

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "Patio",
                keyColumn: "IdPatio",
                keyValue: 47);

            migrationBuilder.DropColumn(
                name: "IdTipoOperacion",
                table: "Patio");

            migrationBuilder.DropColumn(
                name: "IdTipoPatio",
                table: "Patio");

            migrationBuilder.RenameTable(
                name: "Patio",
                newName: "Patios");

            migrationBuilder.AlterColumn<int>(
                name: "IdPatio",
                table: "Patios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Operacion",
                table: "Patios",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Patios",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true);
        }
    }
}
