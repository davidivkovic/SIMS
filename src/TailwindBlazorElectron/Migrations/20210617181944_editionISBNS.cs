using Microsoft.EntityFrameworkCore.Migrations;

namespace TailwindBlazorElectron.Migrations
{
    public partial class editionISBNS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBN13",
                table: "Books",
                newName: "IdTitle");

            migrationBuilder.AddColumn<string>(
                name: "ISBN13",
                table: "Editions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Publisher",
                table: "Editions",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ISBN13",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Editions");

            migrationBuilder.RenameColumn(
                name: "IdTitle",
                table: "Books",
                newName: "ISBN13");
        }
    }
}
