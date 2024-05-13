using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lovecraft.Api.Migrations
{
    public partial class createbadgetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryComments_Stories_StoryId",
                table: "StoryComments");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryComments_Users_UserId",
                table: "StoryComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryComments",
                table: "StoryComments");

            migrationBuilder.RenameTable(
                name: "StoryComments",
                newName: "StoryComment");

            migrationBuilder.RenameIndex(
                name: "IX_StoryComments_UserId",
                table: "StoryComment",
                newName: "IX_StoryComment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryComments_StoryId",
                table: "StoryComment",
                newName: "IX_StoryComment_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryComment",
                table: "StoryComment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Badge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmptyBadgeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BadgeUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Badge", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserBadge",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    BadgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBadge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBadge_Badge_BadgeId",
                        column: x => x.BadgeId,
                        principalTable: "Badge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBadge_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBadge_BadgeId",
                table: "UserBadge",
                column: "BadgeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBadge_UserId",
                table: "UserBadge",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryComment_Stories_StoryId",
                table: "StoryComment",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryComment_Users_UserId",
                table: "StoryComment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoryComment_Stories_StoryId",
                table: "StoryComment");

            migrationBuilder.DropForeignKey(
                name: "FK_StoryComment_Users_UserId",
                table: "StoryComment");

            migrationBuilder.DropTable(
                name: "UserBadge");

            migrationBuilder.DropTable(
                name: "Badge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoryComment",
                table: "StoryComment");

            migrationBuilder.RenameTable(
                name: "StoryComment",
                newName: "StoryComments");

            migrationBuilder.RenameIndex(
                name: "IX_StoryComment_UserId",
                table: "StoryComments",
                newName: "IX_StoryComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_StoryComment_StoryId",
                table: "StoryComments",
                newName: "IX_StoryComments_StoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoryComments",
                table: "StoryComments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StoryComments_Stories_StoryId",
                table: "StoryComments",
                column: "StoryId",
                principalTable: "Stories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoryComments_Users_UserId",
                table: "StoryComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
