using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lovecraft.Api.Migrations
{
    public partial class updatebadgetable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Badge",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Badge");
        }
    }
}
