using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CMS.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Views",
                columns: table => new
                {
                    RazerViewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Location = table.Column<string>(nullable: true),
                    Model = table.Column<string>(nullable: true),
                    HTMLContentId = table.Column<int>(nullable: false),
                    HTMLContent = table.Column<string>(nullable: true),
                    CSSContentId = table.Column<int>(nullable: false),
                    CSSContent = table.Column<string>(nullable: true),
                    JSContentId = table.Column<int>(nullable: false),
                    JSContent = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTimeOffset>(nullable: false),
                    LastRequested = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Views", x => x.RazerViewId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Views");
        }
    }
}
