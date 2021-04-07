using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class addedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_DBUsers_Username",
                table: "DBCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_DBContacts_DBUsers_Username",
                table: "DBContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DBUsers",
                table: "DBUsers");

            migrationBuilder.DropIndex(
                name: "IX_DBContacts_Username",
                table: "DBContacts");

            migrationBuilder.DropIndex(
                name: "IX_DBCarts_Username",
                table: "DBCarts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "DBContacts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "DBCarts");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "DBUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DBUsers",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DBContacts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "DBCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DBUsers",
                table: "DBUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DBContacts_UserId",
                table: "DBContacts",
                column: "UserId",
                unique: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_DBContacts_DBUsers_UserId",
                table: "DBContacts",
                column: "UserId",
                principalTable: "DBUsers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DBCarts_DBUsers_UserId",
                table: "DBCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_DBContacts_DBUsers_UserId",
                table: "DBContacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DBUsers",
                table: "DBUsers");

            migrationBuilder.DropIndex(
                name: "IX_DBContacts_UserId",
                table: "DBContacts");

            migrationBuilder.DropIndex(
                name: "IX_DBCarts_UserId",
                table: "DBCarts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DBUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DBContacts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "DBCarts");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "DBUsers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "DBContacts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "DBCarts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_DBUsers",
                table: "DBUsers",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_DBContacts_Username",
                table: "DBContacts",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DBCarts_Username",
                table: "DBCarts",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_DBCarts_DBUsers_Username",
                table: "DBCarts",
                column: "Username",
                principalTable: "DBUsers",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DBContacts_DBUsers_Username",
                table: "DBContacts",
                column: "Username",
                principalTable: "DBUsers",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
