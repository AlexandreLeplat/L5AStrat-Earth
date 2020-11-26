using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class lobbymode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlaying",
                table: "Players",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "CreatorId",
                table: "Campaigns",
                nullable: false,
                defaultValue: 1L);

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CreatorId",
                table: "Campaigns",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Campaigns_Users_CreatorId",
                table: "Campaigns",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Campaigns_Users_CreatorId",
                table: "Campaigns");

            migrationBuilder.DropIndex(
                name: "IX_Campaigns_CreatorId",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "IsPlaying",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Campaigns");
        }
    }
}
