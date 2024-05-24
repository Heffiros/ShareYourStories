using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lovecraft.Api.Migrations
{
    public partial class createstoryhistorytable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    LastPageReadId = table.Column<int>(type: "int", nullable: false),
                    Reread = table.Column<int>(type: "int", nullable: false),
                    HistoryState = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryHistory_Pages_LastPageReadId",
                        column: x => x.LastPageReadId,
                        principalTable: "Pages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoryHistory_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StoryHistory_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryHistory_LastPageReadId",
                table: "StoryHistory",
                column: "LastPageReadId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryHistory_StoryId",
                table: "StoryHistory",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryHistory_UserId",
                table: "StoryHistory",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryHistory");
        }
    }
}
