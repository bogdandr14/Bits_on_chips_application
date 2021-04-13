using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class addPriceToCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_DBUsers_UserId",
                table: "DBCarts");

            migrationBuilder.DropIndex(
                name: "IX_DBCarts_UserId",
                table: "DBCarts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DBCarts");

            migrationBuilder.AddColumn<float>(
                name: "Price",
                table: "DBCarts",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "DBCarts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DBCarts_UserName",
                table: "DBCarts",
                column: "UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_AspNetUsers_UserName",
                table: "DBCarts",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_AspNetUsers_UserName",
                table: "DBCarts");

            migrationBuilder.DropIndex(
                name: "IX_DBCarts_UserName",
                table: "DBCarts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "DBCarts");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "DBCarts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DBCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DBCarts_UserId",
                table: "DBCarts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_DBUsers_UserId",
                table: "DBCarts",
                column: "UserId",
                principalTable: "DBUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
