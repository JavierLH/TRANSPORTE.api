using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgreganLlavesPKFKADestinosEstadoCuidadYDestinoEstado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdOrigen",
                table: "Origen",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdCiudad",
                table: "DestinoEstadoCiudad",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "IdEstado",
                table: "DestinoEstado",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Origen",
                table: "Origen",
                column: "IdOrigen");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DestinoEstadoCiudad",
                table: "DestinoEstadoCiudad",
                column: "IdCiudad");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DestinoEstado",
                table: "DestinoEstado",
                column: "IdEstado");

            migrationBuilder.InsertData(
                table: "DestinoEstado",
                columns: new[] { "IdEstado", "DestinoEstado" },
                values: new object[,]
                {
                    { 1, "AGUASCALIENTES" },
                    { 2, "BAJA CALIFORNIA" },
                    { 3, "CAMPECHE" },
                    { 4, "CHIAPAS" },
                    { 5, "CHIHUAHUA" },
                    { 6, "COAHUILA" },
                    { 7, "COLIMA" },
                    { 8, "DURANGO" },
                    { 9, "GUANAJUATO" },
                    { 10, "GUERRERO" },
                    { 11, "HIDALGO" },
                    { 12, "JALISCO" },
                    { 13, "MEXICO" },
                    { 14, "MICHOACAN" },
                    { 15, "MORELOS" },
                    { 16, "NAYARIT" },
                    { 17, "NUEVO LEÓN" },
                    { 18, "OAXACA" },
                    { 19, "PUEBLA" },
                    { 20, "QUERETARO" },
                    { 21, "QUINTA ROO" },
                    { 22, "SAN LUIS POTOSÍ" },
                    { 23, "SINALOA" },
                    { 24, "SONORA" },
                    { 25, "TABASCO" },
                    { 26, "TAMAULIPAS" },
                    { 27, "TLAXCALA" },
                    { 28, "VERACRUZ" },
                    { 29, "YUCATAN" },
                    { 30, "ZACATECAS" },
                    { 31, "NUEVO LEON" },
                    { 32, "SAN LUIS POTOSI" }
                });

            migrationBuilder.InsertData(
                table: "Origen",
                columns: new[] { "IdOrigen", "Origen" },
                values: new object[,]
                {
                    { 1, "VER" },
                    { 2, "MAN" }
                });

            migrationBuilder.InsertData(
                table: "DestinoEstadoCiudad",
                columns: new[] { "IdCiudad", "DestinoEstadoCiudad", "IdEstado" },
                values: new object[,]
                {
                    { 1, "AGUASCALIENTES", 1 },
                    { 2, "MEXICALI", 2 },
                    { 3, "Tijuana", 2 },
                    { 4, "Ensenada", 2 },
                    { 5, "Cd. Del Carmen", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DestinoEstadoCiudad_IdEstado",
                table: "DestinoEstadoCiudad",
                column: "IdEstado");

            migrationBuilder.AddForeignKey(
                name: "FK_DestinoEstadoCiudad_DestinoEstado_IdEstado",
                table: "DestinoEstadoCiudad",
                column: "IdEstado",
                principalTable: "DestinoEstado",
                principalColumn: "IdEstado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DestinoEstadoCiudad_DestinoEstado_IdEstado",
                table: "DestinoEstadoCiudad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Origen",
                table: "Origen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DestinoEstadoCiudad",
                table: "DestinoEstadoCiudad");

            migrationBuilder.DropIndex(
                name: "IX_DestinoEstadoCiudad_IdEstado",
                table: "DestinoEstadoCiudad");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DestinoEstado",
                table: "DestinoEstado");

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "DestinoEstadoCiudad",
                keyColumn: "IdCiudad",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DestinoEstadoCiudad",
                keyColumn: "IdCiudad",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DestinoEstadoCiudad",
                keyColumn: "IdCiudad",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DestinoEstadoCiudad",
                keyColumn: "IdCiudad",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DestinoEstadoCiudad",
                keyColumn: "IdCiudad",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Origen",
                keyColumn: "IdOrigen",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Origen",
                keyColumn: "IdOrigen",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DestinoEstado",
                keyColumn: "IdEstado",
                keyValue: 3);

            migrationBuilder.AlterColumn<int>(
                name: "IdOrigen",
                table: "Origen",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdCiudad",
                table: "DestinoEstadoCiudad",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "IdEstado",
                table: "DestinoEstado",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
