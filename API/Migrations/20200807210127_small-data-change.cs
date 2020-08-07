using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class smalldatachange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NextPhase", "Assets" },
                values: new object[] { new DateTime(2020, 8, 8, 23, 1, 26, 604, DateTimeKind.Local).AddTicks(8165), "{\"Classement\":{\"1er\":\"Doji Misao\",\"2\\u00E8me\":\"Kitsuki Hisao\",\"3\\u00E8me\":\"Yogo Rushi\",\"4\\u00E8me\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"Tour 6\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"7\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"2\",\"Influence\":\"1\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"3\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"4\",\"Infamie\":\"2\"},\"Ressources\":{\"Strat\\u00E9gie\":\"4\",\"Influence\":\"0\"}}");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NextPhase", "Assets" },
                values: new object[] { new DateTime(2020, 8, 6, 18, 37, 46, 216, DateTimeKind.Local).AddTicks(6031), "{\"Classement\":{\"1er\":\"Doji Misao\",\"2\\u00E8me\":\"Kitsuki Hisao\",\"3\\u00E8me\":\"Yogo Rushi\",\"4\\u00E8me\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"\":\"Tour 6\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}");
        }
    }
}
