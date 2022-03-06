using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class addedDescriptionForCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "DBCategories",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "DBCategories");
        }
    }
}
