using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TailwindBlazorElectron.Migrations
{
    public partial class authorseditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Editions_EditionId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Authors_EditionId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "EditionId",
                table: "Authors");

            migrationBuilder.CreateTable(
                name: "AuthorEdition",
                columns: table => new
                {
                    AuthorsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EditionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorEdition", x => new { x.AuthorsId, x.EditionsId });
                    table.ForeignKey(
                        name: "FK_AuthorEdition_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorEdition_Editions_EditionsId",
                        column: x => x.EditionsId,
                        principalTable: "Editions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorEdition_EditionsId",
                table: "AuthorEdition",
                column: "EditionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorEdition");

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
    }
}
