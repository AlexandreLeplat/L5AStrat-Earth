using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class maps : Migration
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
                    HomeWidgets = table.Column<string>(nullable: true)
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
                name: "ActionTypes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Label = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Form = table.Column<string>(nullable: true),
                    GameId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActionTypes_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    CurrentPhase = table.Column<int>(nullable: false),
                    CurrentTurn = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
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
                    Color = table.Column<string>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Turn = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    CampaignId = table.Column<long>(nullable: false),
                    PlayerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maps_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maps_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrdersSheets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Turn = table.Column<int>(nullable: false),
                    Priority = table.Column<int>(nullable: false),
                    MaxOrdersCount = table.Column<int>(nullable: false),
                    SendDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    PlayerId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersSheets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdersSheets_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MapTiles",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    X = table.Column<int>(nullable: false),
                    Y = table.Column<int>(nullable: false),
                    Symbol = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    Assets = table.Column<string>(nullable: true),
                    MapId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MapTiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MapTiles_Maps_MapId",
                        column: x => x.MapId,
                        principalTable: "Maps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Parameters = table.Column<string>(nullable: true),
                    Rank = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    ActionTypeId = table.Column<long>(nullable: false),
                    OrdersSheetId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_ActionTypes_ActionTypeId",
                        column: x => x.ActionTypeId,
                        principalTable: "ActionTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Orders_OrdersSheets_OrdersSheetId",
                        column: x => x.OrdersSheetId,
                        principalTable: "OrdersSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    MapTileId = table.Column<long>(nullable: false),
                    PlayerId = table.Column<long>(nullable: false),
                    Assets = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Units_MapTiles_MapTileId",
                        column: x => x.MapTileId,
                        principalTable: "MapTiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Units_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Name", "HomeWidgets" },
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
                table: "ActionTypes",
                columns: new[] { "Id", "Description", "GameId", "Label", "Form" },
                values: new object[,]
                {
                    { 1L, "Gagnez un point de Stratégie", 1L, "Planification", "[{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Manoeuvre sournoise : Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\"}]" },
                    { 2L, "Gagnez 2 points de Gloire et faites-en gagner 2 à un adversaire", 1L, "Flatterie", "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\"}]" },
                    { 3L, "Infligez 3 points d’Infamie à un adversaire et gagnez-en vous-même un point", 1L, "Médisance", "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 2 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\"}]" },
                    { 4L, "Déployez une armée pour 5 points de Stratégie", 1L, "Renforts", "[{\"Label\":\"Case\",\"Type\":3,\"Description\":\"La case de renfort cibl\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Soutien militaire : D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\"}]" },
                    { 5L, "Déplacez une armée d'une case", 1L, "Déplacement", "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\"},{\"Label\":\"Destination\",\"Type\":5,\"Description\":\"La destination de l\\u0027arm\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\"}]" },
                    { 6L, "Changez la formation de l'armée", 1L, "Formation", "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\"},{\"Label\":\"Formation\",\"Type\":6,\"Description\":\"La formation \\u00E0 adopter\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Discipline : D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\"}]" },
                    { 7L, "Subissez un point d'Infamie pour espionner un adversaire", 1L, "Renseignements", "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire \\u00E0 espionner\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"R\\u00E9seau d\\u0027information : D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\"}]" }
                });

            migrationBuilder.InsertData(
                table: "Campaigns",
                columns: new[] { "Id", "CurrentPhase", "CurrentTurn", "GameId", "Name", "NextPhase", "PhaseLength", "Status", "Assets" },
                values: new object[] { 1L, 2, 1, 1L, "La bataille des Quatre Vents", new DateTime(2020, 9, 30, 18, 58, 48, 305, DateTimeKind.Local).AddTicks(510), 24, 2, "{\"Classement\":{\"1er\":\"Doji Misao\",\"2\\u00E8me\":\"Kitsuki Hisao\",\"3\\u00E8me\":\"Yogo Rushi\",\"4\\u00E8me\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"Tour 6\":\"\"}}" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "CampaignId", "Color", "IsCurrentPlayer", "Name", "UserId", "Assets" },
                values: new object[,]
                {
                    { 1L, 1L, null, true, "Admin", 1L, null },
                    { 2L, 1L, "green", true, "Kitsuki Hisao", 2L, "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}" },
                    { 3L, 1L, "cyan", true, "Doji Misao", 3L, "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"7\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"2\",\"Influence\":\"1\"}}" },
                    { 4L, 1L, "yellow", true, "Ikoma Kiyoshi", 4L, "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"3\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}" },
                    { 5L, 1L, "red", true, "Yogo Rushi", 5L, "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"4\",\"Infamie\":\"2\"},\"Ressources\":{\"Strat\\u00E9gie\":\"4\",\"Influence\":\"0\"}}" }
                });

            migrationBuilder.InsertData(
                table: "Maps",
                columns: new[] { "Id", "CampaignId", "CreationDate", "Name", "PlayerId", "Turn" },
                values: new object[] { 1L, 1L, new DateTime(2020, 9, 29, 18, 58, 48, 322, DateTimeKind.Local).AddTicks(7355), "Début de Tour 1", 1L, 1 });

            migrationBuilder.InsertData(
                table: "OrdersSheets",
                columns: new[] { "Id", "MaxOrdersCount", "PlayerId", "Priority", "SendDate", "Status", "Turn" },
                values: new object[,]
                {
                    { 1L, 5, 2L, 0, null, 1, 3 },
                    { 2L, 5, 3L, 0, null, 1, 3 },
                    { 3L, 5, 4L, 0, null, 1, 3 },
                    { 4L, 5, 5L, 0, null, 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "MapTiles",
                columns: new[] { "Id", "Color", "MapId", "Name", "Symbol", "X", "Y", "Assets" },
                values: new object[,]
                {
                    { 1L, "wheat", 1L, "Plaine", "", 0, 0, "{}" },
                    { 60L, "linen", 1L, "Village", "house", 6, 5, "{}" },
                    { 59L, "wheat", 1L, "Plaine", "", 6, 4, "{}" },
                    { 58L, "linen", 1L, "Plaine", "", 6, 3, "{}" },
                    { 57L, "wheat", 1L, "Plaine", "", 6, 2, "{}" },
                    { 56L, "linen", 1L, "Plaine", "", 6, 1, "{}" },
                    { 55L, "wheat", 1L, "Entrée", "home", 6, 0, "{}" },
                    { 54L, "linen", 1L, "Entrée", "home", 5, 8, "{}" },
                    { 61L, "wheat", 1L, "Plaine", "", 6, 6, "{}" },
                    { 53L, "wheat", 1L, "Plaine", "", 5, 7, "{}" },
                    { 51L, "wheat", 1L, "Plaine", "", 5, 5, "{}" },
                    { 50L, "linen", 1L, "Plaine", "", 5, 4, "{}" },
                    { 49L, "wheat", 1L, "Plaine", "", 5, 3, "{}" },
                    { 48L, "linen", 1L, "Village", "house", 5, 2, "{}" },
                    { 47L, "wheat", 1L, "Plaine", "", 5, 1, "{}" },
                    { 46L, "linen", 1L, "Entrée", "home", 5, 0, "{}" },
                    { 45L, "wheat", 1L, "Entrée", "home", 4, 8, "{}" },
                    { 52L, "linen", 1L, "Plaine", "", 5, 6, "{}" },
                    { 62L, "linen", 1L, "Plaine", "", 6, 7, "{}" },
                    { 63L, "wheat", 1L, "Plaine", "", 6, 8, "{}" },
                    { 64L, "linen", 1L, "Plaine", "", 7, 0, "{}" },
                    { 81L, "wheat", 1L, "Plaine", "", 8, 8, "{}" },
                    { 80L, "linen", 1L, "Plaine", "", 8, 7, "{}" },
                    { 79L, "wheat", 1L, "Plaine", "", 8, 6, "{}" },
                    { 78L, "linen", 1L, "Entrée", "home", 8, 5, "{}" },
                    { 77L, "wheat", 1L, "Entrée", "home", 8, 4, "{}" },
                    { 76L, "linen", 1L, "Entrée", "home", 8, 3, "{}" },
                    { 75L, "wheat", 1L, "Entrée", "home", 8, 2, "{}" },
                    { 74L, "linen", 1L, "Plaine", "", 8, 1, "{}" },
                    { 73L, "wheat", 1L, "Plaine", "", 8, 0, "{}" },
                    { 72L, "linen", 1L, "Plaine", "", 7, 8, "{}" },
                    { 71L, "wheat", 1L, "Avant-poste", "tower", 7, 7, "{}" },
                    { 70L, "linen", 1L, "Plaine", "", 7, 6, "{}" },
                    { 69L, "wheat", 1L, "Plaine", "", 7, 5, "{}" },
                    { 68L, "linen", 1L, "Plaine", "", 7, 4, "{}" },
                    { 67L, "wheat", 1L, "Plaine", "", 7, 3, "{}" },
                    { 66L, "linen", 1L, "Plaine", "", 7, 2, "{}" },
                    { 65L, "wheat", 1L, "Plaine", "", 7, 1, "{}" },
                    { 44L, "linen", 1L, "Plaine", "", 4, 7, "{}" },
                    { 42L, "linen", 1L, "Plaine", "", 4, 5, "{}" },
                    { 43L, "wheat", 1L, "Plaine", "", 4, 6, "{}" },
                    { 40L, "linen", 1L, "Plaine", "", 4, 3, "{}" },
                    { 18L, "linen", 1L, "Plaine", "", 1, 8, "{}" },
                    { 17L, "wheat", 1L, "Plaine", "", 1, 7, "{}" },
                    { 16L, "linen", 1L, "Plaine", "", 1, 6, "{}" },
                    { 15L, "wheat", 1L, "Plaine", "", 1, 5, "{}" },
                    { 14L, "linen", 1L, "Plaine", "", 1, 4, "{}" },
                    { 13L, "wheat", 1L, "Plaine", "", 1, 3, "{}" },
                    { 12L, "linen", 1L, "Plaine", "", 1, 2, "{}" },
                    { 11L, "wheat", 1L, "Avant-poste", "tower", 1, 1, "{}" },
                    { 10L, "linen", 1L, "Plaine", "", 1, 0, "{}" },
                    { 9L, "wheat", 1L, "Plaine", "", 0, 8, "{}" },
                    { 8L, "linen", 1L, "Plaine", "", 0, 7, "{}" },
                    { 7L, "wheat", 1L, "Entrée", "home", 0, 6, "{}" },
                    { 6L, "linen", 1L, "Entrée", "home", 0, 5, "{}" },
                    { 5L, "wheat", 1L, "Entrée", "home", 0, 4, "{}" },
                    { 4L, "linen", 1L, "Entrée", "home", 0, 3, "{}" },
                    { 3L, "wheat", 1L, "Plaine", "", 0, 2, "{}" },
                    { 2L, "linen", 1L, "Plaine", "", 0, 1, "{}" },
                    { 19L, "wheat", 1L, "Plaine", "", 2, 0, "{}" },
                    { 41L, "wheat", 1L, "Forteresse", "castle", 4, 4, "{}" },
                    { 20L, "linen", 1L, "Plaine", "", 2, 1, "{}" },
                    { 22L, "linen", 1L, "Village", "house", 2, 3, "{}" },
                    { 39L, "wheat", 1L, "Plaine", "", 4, 2, "{}" },
                    { 38L, "linen", 1L, "Plaine", "", 4, 1, "{}" },
                    { 37L, "wheat", 1L, "Entrée", "home", 4, 0, "{}" },
                    { 36L, "linen", 1L, "Entrée", "home", 3, 8, "{}" },
                    { 35L, "wheat", 1L, "Plaine", "", 3, 7, "{}" },
                    { 34L, "linen", 1L, "Village", "house", 3, 6, "{}" },
                    { 21L, "wheat", 1L, "Plaine", "", 2, 2, "{}" },
                    { 32L, "linen", 1L, "Plaine", "", 3, 4, "{}" },
                    { 33L, "wheat", 1L, "Plaine", "", 3, 5, "{}" },
                    { 30L, "linen", 1L, "Plaine", "", 3, 2, "{}" },
                    { 29L, "wheat", 1L, "Plaine", "", 3, 1, "{}" },
                    { 28L, "linen", 1L, "Entrée", "home", 3, 0, "{}" },
                    { 27L, "wheat", 1L, "Entrée", "home", 2, 8, "{}" },
                    { 26L, "linen", 1L, "Plaine", "", 2, 7, "{}" },
                    { 25L, "wheat", 1L, "Plaine", "", 2, 6, "{}" },
                    { 24L, "linen", 1L, "Plaine", "", 2, 5, "{}" },
                    { 23L, "wheat", 1L, "Plaine", "", 2, 4, "{}" },
                    { 31L, "wheat", 1L, "Plaine", "", 3, 3, "{}" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ActionTypeId", "Comment", "OrdersSheetId", "Rank", "Status", "Parameters" },
                values: new object[,]
                {
                    { 3L, 4L, "+ 1 Infamie, Doji Misao +3 Infamie", 3L, 0, 2, "{\"Cible\":\"3\",\"Augmentation\":\"false\"}" },
                    { 1L, 1L, "+ 1 Strat", 1L, 0, 2, "{\"Augmentation\":\"false\"}" },
                    { 2L, 2L, "+ 2 Gloire, Yogo Rushi + 2 Gloire", 2L, 0, 2, "{\"Cible\":\"5\",\"Augmentation\":\"false\"}" },
                    { 4L, 1L, "+ 1 Strat", 4L, 0, 2, "{\"Augmentation\":\"false\"}" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActionTypes_GameId",
                table: "ActionTypes",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_GameId",
                table: "Campaigns",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_CampaignId",
                table: "Maps",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Maps_PlayerId",
                table: "Maps",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_MapTiles_MapId",
                table: "MapTiles",
                column: "MapId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ActionTypeId",
                table: "Orders",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrdersSheetId",
                table: "Orders",
                column: "OrdersSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersSheets_PlayerId",
                table: "OrdersSheets",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_CampaignId",
                table: "Players",
                column: "CampaignId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_UserId",
                table: "Players",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_MapTileId",
                table: "Units",
                column: "MapTileId");

            migrationBuilder.CreateIndex(
                name: "IX_Units_PlayerId",
                table: "Units",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "ActionTypes");

            migrationBuilder.DropTable(
                name: "OrdersSheets");

            migrationBuilder.DropTable(
                name: "MapTiles");

            migrationBuilder.DropTable(
                name: "Maps");

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
