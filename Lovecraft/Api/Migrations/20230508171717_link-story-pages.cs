using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lovecraft.Api.Migrations
{
    public partial class linkstorypages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoryId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_StoryId",
                table: "Pages",
                column: "StoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_Stories_StoryId",
                table: "Pages",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_Stories_StoryId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_StoryId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "StoryId",
                table: "Pages");
        }
    }
}
