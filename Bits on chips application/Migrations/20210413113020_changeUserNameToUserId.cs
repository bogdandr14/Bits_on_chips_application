using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class changeUserNameToUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_AspNetUsers_UserName",
                table: "DBCarts");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "DBCarts",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_DBCarts_UserName",
                table: "DBCarts",
                newName: "IX_DBCarts_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_AspNetUsers_Id",
                table: "DBCarts",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_AspNetUsers_Id",
                table: "DBCarts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DBCarts",
                newName: "UserName");

            migrationBuilder.RenameIndex(
                name: "IX_DBCarts_Id",
                table: "DBCarts",
                newName: "IX_DBCarts_UserName");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_AspNetUsers_UserName",
                table: "DBCarts",
                column: "UserName",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
