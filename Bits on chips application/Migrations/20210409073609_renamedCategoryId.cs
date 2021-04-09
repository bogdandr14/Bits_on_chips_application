using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class renamedCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBComponents_DBCategories_categoryId",
                table: "DBComponents");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "DBComponents",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DBComponents_categoryId",
                table: "DBComponents",
                newName: "IX_DBComponents_CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryID",
                table: "DBCategories",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBComponents_DBCategories_CategoryId",
                table: "DBComponents",
                column: "CategoryId",
                principalTable: "DBCategories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBComponents_DBCategories_CategoryId",
                table: "DBComponents");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "DBComponents",
                newName: "categoryId");

            migrationBuilder.RenameIndex(
                name: "IX_DBComponents_CategoryId",
                table: "DBComponents",
                newName: "IX_DBComponents_categoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "DBCategories",
                newName: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_DBComponents_DBCategories_categoryId",
                table: "DBComponents",
                column: "categoryId",
                principalTable: "DBCategories",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
