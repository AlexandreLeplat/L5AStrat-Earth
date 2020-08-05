using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class jsoncolumnrename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Widgets",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "HomeWidgets",
                table: "Games",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NextPhase", "Assets" },
                values: new object[] { new DateTime(2020, 8, 6, 10, 56, 8, 629, DateTimeKind.Local).AddTicks(9353), "{\"Classement\":{\"1er\":\"Doji Misao\",\"2\\u00E8me\":\"Kitsuki Hisao\",\"3\\u00E8me\":\"Yogo Rushi\",\"4\\u00E8me\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"\":\"Tour 6\"}}" });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HomeWidgets",
                value: "[\"Clock\",\"PlayerInfo\",\"CampaignInfo\"]");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Assets",
                value: "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeWidgets",
                table: "Games");

            migrationBuilder.AddColumn<string>(
                name: "Widgets",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Assets", "NextPhase" },
                values: new object[] { "{\"Classement\":{\"1er\":\"Doji Misao\",\"2ème\":\"Kitsuki Hisao\",\"3ème\":\"Yogo Rushi\",\"4ème\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"\":\"Tour 6\"}}", new DateTime(2020, 8, 6, 9, 54, 26, 298, DateTimeKind.Local).AddTicks(4866) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Widgets",
                value: "[\"Clock\",\"PlayerInfo\",\"CampaignInfo\"]");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Assets",
                value: "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Assets",
                value: "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Assets",
                value: "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Assets",
                value: "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}");
        }
    }
}
