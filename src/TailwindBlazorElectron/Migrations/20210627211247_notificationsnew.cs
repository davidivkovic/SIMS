using Microsoft.EntityFrameworkCore.Migrations;

namespace TailwindBlazorElectron.Migrations
{
    public partial class notificationsnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SSN",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SSN",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Notifications");
        }
    }
}
