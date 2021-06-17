using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailwindBlazorElectron.Migrations
{
    public partial class editions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YearPublished",
                table: "Editions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Editions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EditionId",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_EditionId",
                table: "Authors",
                column: "EditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Editions_EditionId",
                table: "Authors",
                column: "EditionId",
                principalTable: "Editions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Editions_EditionId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_EditionId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Editions");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Authors");

            migrationBuilder.AlterColumn<DateTime>(
                name: "YearPublished",
                table: "Editions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
