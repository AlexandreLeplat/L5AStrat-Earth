using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class playerstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlaying",
                table: "Players");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Players",
                nullable: false,
                defaultValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Players");

            migrationBuilder.AddColumn<bool>(
                name: "IsPlaying",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
