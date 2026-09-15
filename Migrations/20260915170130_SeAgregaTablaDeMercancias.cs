using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SeAgregaTablaDeMercancias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mercancias",
                columns: table => new
                {
                    CodigoMercancia = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mercancias", x => x.CodigoMercancia);
                });

            migrationBuilder.InsertData(
                table: "Mercancias",
                columns: new[] { "CodigoMercancia", "Nombre" },
                values: new object[,]
                {
                    { 1, "GRAL" },
                    { 2, "DGR" },
                    { 3, "REEFER" },
                    { 4, "DGR&REEFER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mercancias");
        }
    }
}
