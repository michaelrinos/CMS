using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class SplitContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Views",
                newName: "JSContent");

            migrationBuilder.AddColumn<string>(
                name: "CSSContent",
                table: "Views",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HTMLContent",
                table: "Views",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CSSContent",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "HTMLContent",
                table: "Views");

            migrationBuilder.RenameColumn(
                name: "JSContent",
                table: "Views",
                newName: "Content");
        }
    }
}
