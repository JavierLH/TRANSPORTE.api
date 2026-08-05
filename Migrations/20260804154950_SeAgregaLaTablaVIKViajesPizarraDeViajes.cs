using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaLaTablaVIKViajesPizarraDeViajes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VIK_01_Viajes",
                columns: table => new
                {
                    nIdGuia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nViaje = table.Column<int>(type: "int", nullable: true),
                    sEstado = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    nLiquidacion = table.Column<int>(type: "int", nullable: true),
                    sTractor = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    sOperador = table.Column<string>(type: "varchar(80)", unicode: false, maxLength: 80, nullable: true),
                    dFechaIda = table.Column<DateOnly>(type: "date", nullable: true),
                    dFechaVuelta = table.Column<DateOnly>(type: "date", nullable: true),
                    sConfig = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    sCliente = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    sClienteEntrega = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    sConvenio = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    sTipo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    sOrigen = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    sDestino = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    sComentarioViaje = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    dFechaGuia = table.Column<DateOnly>(type: "date", nullable: true),
                    fFletes = table.Column<double>(type: "float", nullable: true),
                    fCasetas = table.Column<double>(type: "float", nullable: true),
                    fSeguros = table.Column<double>(type: "float", nullable: true),
                    fOtros = table.Column<double>(type: "float", nullable: true),
                    fSubtotal = table.Column<double>(type: "float", nullable: true),
                    fIVA = table.Column<double>(type: "float", nullable: true),
                    fRetencion = table.Column<double>(type: "float", nullable: true),
                    fTotalGuia = table.Column<double>(type: "float", nullable: true),
                    nCveCliente = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    dFechaFactura = table.Column<DateOnly>(type: "date", nullable: true),
                    sFactura = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    sEvidencia = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    fFletesFact = table.Column<double>(type: "float", nullable: true),
                    fCasetasFact = table.Column<double>(type: "float", nullable: true),
                    fSegurosFact = table.Column<double>(type: "float", nullable: true),
                    fOtrosFact = table.Column<double>(type: "float", nullable: true),
                    fSubtotalFactura = table.Column<double>(type: "float", nullable: true),
                    fTotalFactura = table.Column<double>(type: "float", nullable: true),
                    sCarga = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    sContenedor = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    sPedimento = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    sAgencia = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    sMercancia = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    sObservacionesRef = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    sObservacionesProd = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    sDespacha = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    sClienteOrigen = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VIK_01_Viajes", x => x.nIdGuia);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VIK_01_Viajes");
        }
    }
}
