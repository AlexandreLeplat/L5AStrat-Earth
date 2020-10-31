using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class adminPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Units_MapTiles_MapTileId",
                table: "Units");

            migrationBuilder.DropIndex(
                name: "IX_Units_MapTileId",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "MapTileId",
                table: "Units");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Units",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "X",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Y",
                table: "Units",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Players",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 10, 15, 16, 37, 22, 656, DateTimeKind.Local).AddTicks(3703));

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 14, 16, 37, 22, 675, DateTimeKind.Local).AddTicks(728));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1L,
                column: "IsAdmin",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "X",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "Y",
                table: "Units");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Players");

            migrationBuilder.AddColumn<long>(
                name: "MapTileId",
                table: "Units",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 10, 7, 22, 35, 18, 752, DateTimeKind.Local).AddTicks(1109));

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 6, 22, 35, 18, 768, DateTimeKind.Local).AddTicks(3297));

            migrationBuilder.CreateIndex(
                name: "IX_Units_MapTileId",
                table: "Units",
                column: "MapTileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Units_MapTiles_MapTileId",
                table: "Units",
                column: "MapTileId",
                principalTable: "MapTiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
