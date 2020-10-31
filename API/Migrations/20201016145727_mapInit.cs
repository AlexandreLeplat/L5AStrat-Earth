using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class mapInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NextPhase", "PhaseLength" },
                values: new object[] { new DateTime(2020, 10, 17, 16, 57, 26, 395, DateTimeKind.Local).AddTicks(7650), 1440 });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Plaine (A0)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "Plaine (A1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Plaine (A2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (A3)", "{\"Propri\\u00E9taire\":{\"Kitsuki Hisao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (A4)", "{\"Propri\\u00E9taire\":{\"Kitsuki Hisao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (A5)", "{\"Propri\\u00E9taire\":{\"Kitsuki Hisao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (A6)", "{\"Propri\\u00E9taire\":{\"Kitsuki Hisao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Name",
                value: "Plaine (A7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Name",
                value: "Plaine (A8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Name",
                value: "Plaine (B0)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Avant-poste (B1)", "{\"Production\":{\"Gloire\":\"1\",\"Strat\\u00E9gie\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Name",
                value: "Plaine (B2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Name",
                value: "Plaine (B3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Name",
                value: "Plaine (B4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Name",
                value: "Plaine (B5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Name",
                value: "Plaine (B6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Name",
                value: "Plaine (B7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Name",
                value: "Plaine (B8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Name",
                value: "Plaine (C0)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Name",
                value: "Plaine (C1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Name",
                value: "Plaine (C2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village (C3)", "{\"Production\":{\"Gloire\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Name",
                value: "Plaine (C4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Name",
                value: "Plaine (C5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Name",
                value: "Plaine (C6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Name",
                value: "Plaine (C7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (C8)", "{\"Propri\\u00E9taire\":{\"Yogo Rushi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (D0)", "{\"Propri\\u00E9taire\":{\"Ikoma Kiyoshi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Name",
                value: "Plaine (D1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Name",
                value: "Plaine (D2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Name",
                value: "Plaine (D3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 32L,
                column: "Name",
                value: "Plaine (D4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 33L,
                column: "Name",
                value: "Plaine (D5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village (D6)", "{\"Production\":{\"Gloire\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 35L,
                column: "Name",
                value: "Plaine (D7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (D8)", "{\"Propri\\u00E9taire\":{\"Yogo Rushi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (E0)", "{\"Propri\\u00E9taire\":{\"Ikoma Kiyoshi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 38L,
                column: "Name",
                value: "Plaine (E1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Name",
                value: "Plaine (E2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 40L,
                column: "Name",
                value: "Plaine (E3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Forteresse (E4)", "{\"Production\":{\"Gloire\":\"3\",\"Strat\\u00E9gie\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Name",
                value: "Plaine (E5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Name",
                value: "Plaine (E6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Name",
                value: "Plaine (E7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (E8)", "{\"Propri\\u00E9taire\":{\"Yogo Rushi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 46L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (F0)", "{\"Propri\\u00E9taire\":{\"Ikoma Kiyoshi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Name",
                value: "Plaine (F1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village (F2)", "{\"Production\":{\"Gloire\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Name",
                value: "Plaine (F3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Name",
                value: "Plaine (F4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Name",
                value: "Plaine (F5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Name",
                value: "Plaine (F6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Name",
                value: "Plaine (F7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 54L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (F8)", "{\"Propri\\u00E9taire\":{\"Yogo Rushi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 55L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (G0)", "{\"Propri\\u00E9taire\":{\"Ikoma Kiyoshi\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Name",
                value: "Plaine (G1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Name",
                value: "Plaine (G2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Name",
                value: "Plaine (G3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 59L,
                column: "Name",
                value: "Plaine (G4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village (G5)", "{\"Production\":{\"Gloire\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 61L,
                column: "Name",
                value: "Plaine (G6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Name",
                value: "Plaine (G7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Name",
                value: "Plaine (G8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Name",
                value: "Plaine (H0)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Name",
                value: "Plaine (H1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 66L,
                column: "Name",
                value: "Plaine (H2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Name",
                value: "Plaine (H3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 68L,
                column: "Name",
                value: "Plaine (H4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 69L,
                column: "Name",
                value: "Plaine (H5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Name",
                value: "Plaine (H6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Avant-poste (H7)", "{\"Production\":{\"Gloire\":\"1\",\"Strat\\u00E9gie\":\"2\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Name",
                value: "Plaine (H8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 73L,
                column: "Name",
                value: "Plaine (I0)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Name",
                value: "Plaine (I1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (I2)", "{\"Propri\\u00E9taire\":{\"Doji Misao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (I3)", "{\"Propri\\u00E9taire\":{\"Doji Misao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (I4)", "{\"Propri\\u00E9taire\":{\"Doji Misao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 78L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée (I5)", "{\"Propri\\u00E9taire\":{\"Doji Misao\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Name",
                value: "Plaine (I6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 80L,
                column: "Name",
                value: "Plaine (I7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Name",
                value: "Plaine (I8)");

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 16, 16, 57, 26, 413, DateTimeKind.Local).AddTicks(35));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "NextPhase", "PhaseLength" },
                values: new object[] { new DateTime(2020, 10, 15, 16, 37, 22, 656, DateTimeKind.Local).AddTicks(3703), 24 });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Avant-poste", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 32L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 33L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 35L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 38L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 40L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Forteresse", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 46L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 54L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 55L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 59L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Village", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 61L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 66L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 68L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 69L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Avant-poste", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 73L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 78L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Entrée", "{}" });

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 80L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Name",
                value: "Plaine");

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 14, 16, 37, 22, 675, DateTimeKind.Local).AddTicks(728));
        }
    }
}
