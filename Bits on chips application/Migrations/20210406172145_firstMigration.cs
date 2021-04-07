using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DBCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "DBUsers",
                columns: table => new
                {
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBUsers", x => x.Username);
                });

            migrationBuilder.CreateTable(
                name: "DBComponents",
                columns: table => new
                {
                    ComponentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Interface = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Buffer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Socket = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxRead = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Efficiency = table.Column<float>(type: "real", nullable: false),
                    Technology = table.Column<int>(type: "int", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    NumberCores = table.Column<int>(type: "int", nullable: false),
                    NumberSlots = table.Column<int>(type: "int", nullable: false),
                    NumberFans = table.Column<int>(type: "int", nullable: false),
                    categoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBComponents", x => x.ComponentId);
                    table.ForeignKey(
                        name: "FK_DBComponents_DBCategories_categoryId",
                        column: x => x.categoryId,
                        principalTable: "DBCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DBContacts",
                columns: table => new
                {
                    ContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBContacts", x => x.ContactID);
                    table.ForeignKey(
                        name: "FK_DBContacts_DBUsers_Username",
                        column: x => x.Username,
                        principalTable: "DBUsers",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DBCarts",
                columns: table => new
                {
                    CartItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DBCarts", x => x.CartItemId);
                    table.ForeignKey(
                        name: "FK_DBCarts_DBComponents_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "DBComponents",
                        principalColumn: "ComponentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DBCarts_DBUsers_Username",
                        column: x => x.Username,
                        principalTable: "DBUsers",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DBCarts_ComponentId",
                table: "DBCarts",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_DBCarts_Username",
                table: "DBCarts",
                column: "Username");

            migrationBuilder.CreateIndex(
                name: "IX_DBComponents_categoryId",
                table: "DBComponents",
                column: "categoryId");

            migrationBuilder.CreateIndex(
                name: "IX_DBContacts_Username",
                table: "DBContacts",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DBCarts");

            migrationBuilder.DropTable(
                name: "DBContacts");

            migrationBuilder.DropTable(
                name: "DBComponents");

            migrationBuilder.DropTable(
                name: "DBUsers");

            migrationBuilder.DropTable(
                name: "DBCategories");
        }
    }
}
