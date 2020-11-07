using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class maxpriority : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPriority",
                table: "OrdersSheets",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPriority",
                table: "OrdersSheets");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 11, 6, 6, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 11, 5, 13, 51, 57, 342, DateTimeKind.Local).AddTicks(8852));
        }
    }
}
