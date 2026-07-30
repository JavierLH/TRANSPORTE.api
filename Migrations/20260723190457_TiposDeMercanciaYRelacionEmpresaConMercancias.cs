using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class TiposDeMercanciaYRelacionEmpresaConMercancias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdTipoMercancia",
                table: "TipoMercancia",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoMercancia",
                table: "TipoMercancia",
                column: "IdTipoMercancia");

            migrationBuilder.InsertData(
                table: "TipoMercancia",
                columns: new[] { "IdTipoMercancia", "descripcion", "IdEmpresa" },
                values: new object[,]
                {
                    { 1, "GENERAL", 1 },
                    { 2, "REFRIGERADA", 1 },
                    { 3, "PELIGROSA", 1 },
                    { 4, "REFRIGERADA/PELIGROSA", 1 },
                    { 5, "IMO/GENERAL", 1 },
                    { 6, "REFRIGERADA/GENERAL", 1 },
                    { 7, "GENERAL", 2 },
                    { 8, "REFRIGERADA", 2 },
                    { 9, "PELIGROSA", 2 },
                    { 10, "REFRIGERADA/PELIGROSA", 2 },
                    { 11, "IMO/GENERAL", 2 },
                    { 12, "REFRIGERADA/GENERAL", 2 },
                    { 13, "GENERAL", 3 },
                    { 14, "REFRIGERADA", 3 },
                    { 15, "PELIGROSA", 3 },
                    { 16, "REFRIGERADA/PELIGROSA", 3 },
                    { 17, "IMO/GENERAL", 3 },
                    { 18, "REFRIGERADA/GENERAL", 3 },
                    { 19, "GENERAL", 4 },
                    { 20, "REFRIGERADA", 4 },
                    { 21, "PELIGROSA", 4 },
                    { 22, "REFRIGERADA/PELIGROSA", 4 },
                    { 23, "IMO/GENERAL", 4 },
                    { 24, "REFRIGERADA/GENERAL", 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TipoMercancia_IdEmpresa",
                table: "TipoMercancia",
                column: "IdEmpresa");

            migrationBuilder.AddForeignKey(
                name: "FK_TipoMercancia_Empresa_IdEmpresa",
                table: "TipoMercancia",
                column: "IdEmpresa",
                principalTable: "Empresa",
                principalColumn: "IdEmpresa");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TipoMercancia_Empresa_IdEmpresa",
                table: "TipoMercancia");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoMercancia",
                table: "TipoMercancia");

            migrationBuilder.DropIndex(
                name: "IX_TipoMercancia_IdEmpresa",
                table: "TipoMercancia");

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "TipoMercancia",
                keyColumn: "IdTipoMercancia",
                keyValue: 24);

            migrationBuilder.AlterColumn<int>(
                name: "IdTipoMercancia",
                table: "TipoMercancia",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
