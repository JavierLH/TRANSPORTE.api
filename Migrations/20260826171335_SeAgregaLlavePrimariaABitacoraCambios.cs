using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaLlavePrimariaABitacoraCambios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // La tabla se creó sin llave primaria y nunca se usó, así que se tira y se
            // recrea en vez de alterarla: SQL Server no permite agregar IDENTITY con
            // ALTER COLUMN, y aquí no hay datos ni FKs que rescatar.
            //
            // El DROP va como SQL y no con migrationBuilder.DropTable a propósito:
            // si EF ve un DropTable y un CreateTable de la MISMA tabla en la misma
            // migración, su lógica de tablas temporales falla al armar el
            // diccionario de tablas y lanza "The given key '(BitacoraCambios, )'
            // was not present in the dictionary".
            migrationBuilder.Sql("DROP TABLE dbo.BitacoraCambios;");

            migrationBuilder.CreateTable(
                name: "BitacoraCambios",
                columns: table => new
                {
                    IdBitacora = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tabla = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdRegistro = table.Column<int>(type: "int", nullable: true),
                    Campo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ValorAnterior = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ValorNuevo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaCambio = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BitacoraCambios", x => x.IdBitacora);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BitacoraCambios_Tabla_IdRegistro",
                table: "BitacoraCambios",
                columns: new[] { "Tabla", "IdRegistro" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Mismo motivo que en Up: el DROP va como SQL.
            migrationBuilder.Sql("DROP TABLE dbo.BitacoraCambios;");

            migrationBuilder.CreateTable(
                name: "BitacoraCambios",
                columns: table => new
                {
                    IdBitacora = table.Column<int>(type: "int", nullable: true),
                    Tabla = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    IdRegistro = table.Column<int>(type: "int", nullable: true),
                    Campo = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ValorAnterior = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    ValorNuevo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IdUsuario = table.Column<int>(type: "int", nullable: true),
                    FechaCambio = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table => { });
        }
    }
}
