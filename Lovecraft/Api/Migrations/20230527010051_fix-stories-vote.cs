using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lovecraft.Api.Migrations
{
    public partial class fixstoriesvote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryVote_Stories_StoryId",
                table: "StoryVote");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryVote_Users_UserId",
                table: "StoryVote");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryVote",
                table: "StoryVote");

            migrationBuilder.RenameTable(
                name: "StoryVote",
                newName: "StoryVotes");

            migrationBuilder.RenameIndex(
                name: "IX_StoryVote_UserId",
                table: "StoryVotes",
                newName: "IX_StoryVotes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryVote_StoryId",
                table: "StoryVotes",
                newName: "IX_StoryVotes_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryVotes",
                table: "StoryVotes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryVotes_Stories_StoryId",
                table: "StoryVotes",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryVotes_Users_UserId",
                table: "StoryVotes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryVotes_Stories_StoryId",
                table: "StoryVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryVotes_Users_UserId",
                table: "StoryVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryVotes",
                table: "StoryVotes");

            migrationBuilder.RenameTable(
                name: "StoryVotes",
                newName: "StoryVote");

            migrationBuilder.RenameIndex(
                name: "IX_StoryVotes_UserId",
                table: "StoryVote",
                newName: "IX_StoryVote_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryVotes_StoryId",
                table: "StoryVote",
                newName: "IX_StoryVote_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryVote",
                table: "StoryVote",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryVote_Stories_StoryId",
                table: "StoryVote",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryVote_Users_UserId",
                table: "StoryVote",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
