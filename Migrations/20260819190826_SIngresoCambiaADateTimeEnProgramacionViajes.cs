using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIV2.Migrations
{
    /// <inheritdoc />
    public partial class SIngresoCambiaADateTimeEnProgramacionViajes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "sIngreso",
                table: "ProgramacionViajes",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldUnicode: false,
                oldMaxLength: 100,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "sIngreso",
                table: "ProgramacionViajes",
                type: "varchar(100)",
                unicode: false,
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime",
                oldNullable: true);
        }
    }
}
