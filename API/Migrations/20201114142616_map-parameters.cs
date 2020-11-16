using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class mapparameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_jsonActions",
                table: "MapTiles");

            migrationBuilder.AddColumn<long>(
                name: "PreviousMessageId",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Turn",
                table: "Messages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "_jsonParameters",
                table: "MapTiles",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 11, 15, 6, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 11, 14, 15, 26, 15, 527, DateTimeKind.Local).AddTicks(223));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PreviousMessageId",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Turn",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "_jsonParameters",
                table: "MapTiles");

            migrationBuilder.AddColumn<string>(
                name: "_jsonActions",
                table: "MapTiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 11, 7, 6, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 11, 6, 22, 18, 42, 868, DateTimeKind.Local).AddTicks(7176));
        }
    }
}
