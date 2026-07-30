using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class TablaEquipoEmpresaCompartido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "IdEquipo",
                table: "Equipos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Equipos",
                table: "Equipos",
                column: "IdEquipo");

            migrationBuilder.CreateTable(
                name: "EquipoEmpresaCompartido",
                columns: table => new
                {
                    IdEquipoEmpresaCompartido = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdEquipo = table.Column<int>(type: "int", nullable: false),
                    IdEmpresa = table.Column<int>(type: "int", nullable: false),
                    Estatus = table.Column<int>(type: "int", nullable: true),
                    FechaAgregado = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoEmpresaCompartido", x => x.IdEquipoEmpresaCompartido);
                    table.ForeignKey(
                        name: "FK_EquipoEmpresaCompartido_Empresa_IdEmpresa",
                        column: x => x.IdEmpresa,
                        principalTable: "Empresa",
                        principalColumn: "IdEmpresa",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoEmpresaCompartido_Equipos_IdEquipo",
                        column: x => x.IdEquipo,
                        principalTable: "Equipos",
                        principalColumn: "IdEquipo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipoEmpresaCompartido_IdEmpresa",
                table: "EquipoEmpresaCompartido",
                column: "IdEmpresa");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoEmpresaCompartido_IdEquipo",
                table: "EquipoEmpresaCompartido",
                column: "IdEquipo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EquipoEmpresaCompartido");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Equipos",
                table: "Equipos");

            migrationBuilder.AlterColumn<int>(
                name: "IdEquipo",
                table: "Equipos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
