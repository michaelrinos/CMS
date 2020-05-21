using Microsoft.EntityFrameworkCore.Migrations;

namespace SportsStore.Migrations
{
    public partial class splitcontents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CSSContentId",
                table: "Views",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HTMLContentId",
                table: "Views",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JSContentId",
                table: "Views",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CSSContentId",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "HTMLContentId",
                table: "Views");

            migrationBuilder.DropColumn(
                name: "JSContentId",
                table: "Views");
        }
    }
}
