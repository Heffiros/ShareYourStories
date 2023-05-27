using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lovecraft.Api.Migrations
{
    public partial class createstoryvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StoryVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoryId = table.Column<int>(type: "int", nullable: false),
                    DateVoted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StoryVote_Stories_StoryId",
                        column: x => x.StoryId,
                        principalTable: "Stories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoryVote_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoryVote_StoryId",
                table: "StoryVote",
                column: "StoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoryVote_UserId",
                table: "StoryVote",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoryVote");
        }
    }
}
