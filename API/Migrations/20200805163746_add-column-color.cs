using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addcolumncolor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Players",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CurrentPhase",
                table: "Campaigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CurrentPhase", "NextPhase" },
                values: new object[] { 2, new DateTime(2020, 8, 6, 18, 37, 46, 216, DateTimeKind.Local).AddTicks(6031) });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Color",
                value: "green");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Color",
                value: "cyan");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Color",
                value: "yellow");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Color",
                value: "red");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "CurrentPhase",
                table: "Campaigns");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 8, 6, 10, 56, 8, 629, DateTimeKind.Local).AddTicks(9353));
        }
    }
}
