using Microsoft.EntityFrameworkCore.Migrations;

namespace Bits_on_chips_application.Migrations
{
    public partial class hugeComponentRefactor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Buffer",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Efficiency",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Interface",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "MaxRead",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "NumberCores",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "NumberFans",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "NumberSlots",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Socket",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Speed",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Technology",
                table: "DBComponents");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "DBComponents");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Buffer",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Efficiency",
                table: "DBComponents",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interface",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaxRead",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberCores",
                table: "DBComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberFans",
                table: "DBComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberSlots",
                table: "DBComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "DBComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Socket",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Speed",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Technology",
                table: "DBComponents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "DBComponents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
