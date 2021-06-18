using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailwindBlazorElectron.Migrations
{
    public partial class reservations1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PickedUpAt",
                table: "Reservations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PickedUpAt",
                table: "Reservations");
        }
    }
}
