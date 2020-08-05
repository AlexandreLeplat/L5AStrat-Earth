using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Widgets = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NextPhase = table.Column<DateTime>(nullable: false),
                    PhaseLength = table.Column<int>(nullable: false),
                    CurrentTurn = table.Column<int>(nullable: false),
                    Assets = table.Column<string>(nullable: true),
                    GameId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Campaigns_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Assets = table.Column<string>(nullable: true),
                    IsCurrentPlayer = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    CampaignId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Name", "Widgets" },
                values: new object[] { 1L, "L5A Strat - TERRE", "[\"Clock\",\"PlayerInfo\",\"CampaignInfo\"]" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1L, "Admin", "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS", 1 },
                    { 2L, "Dragon", "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla", 0 },
                    { 3L, "Grue", "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu", 0 },
                    { 4L, "Lion", "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie", 0 },
                    { 5L, "Scorpion", "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O", 0 }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Id", "Assets", "CurrentTurn", "GameId", "Name", "NextPhase", "PhaseLength" },
                values: new object[] { 1L, "{\"Classement\":{\"1er\":\"Doji Misao\",\"2ème\":\"Kitsuki Hisao\",\"3ème\":\"Yogo Rushi\",\"4ème\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"\":\"Tour 6\"}}", 3, 1L, "La bataille des Quatre Vents", new DateTime(2020, 8, 6, 9, 54, 26, 298, DateTimeKind.Local).AddTicks(4866), 24 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Assets", "CampaignId", "IsCurrentPlayer", "Name", "UserId" },
                values: new object[,]
                {
                    { 1L, null, 1L, true, "Admin", 1L },
                    { 2L, "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}", 1L, true, "Kitsuki Hisao", 2L },
                    { 3L, "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}", 1L, true, "Doji Misao", 3L },
                    { 4L, "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}", 1L, true, "Ikoma Kiyoshi", 4L },
                    { 5L, "{\"Caractéristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Stratégie\":\"3\",\"Influence\":\"0\"}}", 1L, true, "Yogo Rushi", 5L }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_GameId",
                table: "Campaigns",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CampaignId",
                table: "Players",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Campaigns");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
