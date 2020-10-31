using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class orderInputParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Form",
                value: "[{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Manoeuvre sournoise : Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 2 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Form",
                value: "[{\"Label\":\"Case\",\"Type\":\"EntryTile\",\"Description\":\"La case de renfort cibl\\u00E9e\",\"Parameter\":null},{\"Label\":\"Formation\",\"Type\":\"Formation\",\"Description\":\"La formation \\u00E0 adopter\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Soutien militaire : D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\",\"Parameter\":null},{\"Label\":\"Destination\",\"Type\":\"Move\",\"Description\":\"La destination de l\\u0027arm\\u00E9e\",\"Parameter\":\"Arm\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\",\"Parameter\":null},{\"Label\":\"Formation\",\"Type\":\"Formation\",\"Description\":\"La formation \\u00E0 adopter\",\"Parameter\":\"Arm\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Discipline : D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire \\u00E0 espionner\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"R\\u00E9seau d\\u0027information : D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 10, 19, 22, 50, 31, 148, DateTimeKind.Local).AddTicks(8986));

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Name",
                value: "Plaine (A1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Name",
                value: "Plaine (A2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Name",
                value: "Plaine (A3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Name",
                value: "Entrée (A4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Name",
                value: "Entrée (A5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Name",
                value: "Entrée (A6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Name",
                value: "Entrée (A7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 8L,
                column: "Name",
                value: "Plaine (A8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 9L,
                column: "Name",
                value: "Plaine (A9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 10L,
                column: "Name",
                value: "Plaine (B1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 11L,
                column: "Name",
                value: "Avant-poste (B2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 12L,
                column: "Name",
                value: "Plaine (B3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 13L,
                column: "Name",
                value: "Plaine (B4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 14L,
                column: "Name",
                value: "Plaine (B5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 15L,
                column: "Name",
                value: "Plaine (B6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Name",
                value: "Plaine (B7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Name",
                value: "Plaine (B8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Name",
                value: "Plaine (B9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Name",
                value: "Plaine (C1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 20L,
                column: "Name",
                value: "Plaine (C2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 21L,
                column: "Name",
                value: "Plaine (C3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 22L,
                column: "Name",
                value: "Village (C4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 23L,
                column: "Name",
                value: "Plaine (C5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 24L,
                column: "Name",
                value: "Plaine (C6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 25L,
                column: "Name",
                value: "Plaine (C7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 26L,
                column: "Name",
                value: "Plaine (C8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 27L,
                column: "Name",
                value: "Entrée (C9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Name",
                value: "Entrée (D1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 29L,
                column: "Name",
                value: "Plaine (D2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 30L,
                column: "Name",
                value: "Plaine (D3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 31L,
                column: "Name",
                value: "Plaine (D4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 32L,
                column: "Name",
                value: "Plaine (D5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 33L,
                column: "Name",
                value: "Plaine (D6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 34L,
                column: "Name",
                value: "Village (D7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 35L,
                column: "Name",
                value: "Plaine (D8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 36L,
                column: "Name",
                value: "Entrée (D9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 37L,
                column: "Name",
                value: "Entrée (E1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 38L,
                column: "Name",
                value: "Plaine (E2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 39L,
                column: "Name",
                value: "Plaine (E3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 40L,
                column: "Name",
                value: "Plaine (E4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 41L,
                column: "Name",
                value: "Forteresse (E5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 42L,
                column: "Name",
                value: "Plaine (E6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 43L,
                column: "Name",
                value: "Plaine (E7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 44L,
                column: "Name",
                value: "Plaine (E8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 45L,
                column: "Name",
                value: "Entrée (E9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 46L,
                column: "Name",
                value: "Entrée (F1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 47L,
                column: "Name",
                value: "Plaine (F2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 48L,
                column: "Name",
                value: "Village (F3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 49L,
                column: "Name",
                value: "Plaine (F4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 50L,
                column: "Name",
                value: "Plaine (F5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 51L,
                column: "Name",
                value: "Plaine (F6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 52L,
                column: "Name",
                value: "Plaine (F7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 53L,
                column: "Name",
                value: "Plaine (F8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 54L,
                column: "Name",
                value: "Entrée (F9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 55L,
                column: "Name",
                value: "Entrée (G1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 56L,
                column: "Name",
                value: "Plaine (G2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 57L,
                column: "Name",
                value: "Plaine (G3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 58L,
                column: "Name",
                value: "Plaine (G4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 59L,
                column: "Name",
                value: "Plaine (G5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 60L,
                column: "Name",
                value: "Village (G6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 61L,
                column: "Name",
                value: "Plaine (G7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 62L,
                column: "Name",
                value: "Plaine (G8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 63L,
                column: "Name",
                value: "Plaine (G9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 64L,
                column: "Name",
                value: "Plaine (H1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 65L,
                column: "Name",
                value: "Plaine (H2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 66L,
                column: "Name",
                value: "Plaine (H3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 67L,
                column: "Name",
                value: "Plaine (H4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 68L,
                column: "Name",
                value: "Plaine (H5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 69L,
                column: "Name",
                value: "Plaine (H6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 70L,
                column: "Name",
                value: "Plaine (H7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 71L,
                column: "Name",
                value: "Avant-poste (H8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 72L,
                column: "Name",
                value: "Plaine (H9)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 73L,
                column: "Name",
                value: "Plaine (I1)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 74L,
                column: "Name",
                value: "Plaine (I2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 75L,
                column: "Name",
                value: "Entrée (I3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 76L,
                column: "Name",
                value: "Entrée (I4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 77L,
                column: "Name",
                value: "Entrée (I5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 78L,
                column: "Name",
                value: "Entrée (I6)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 79L,
                column: "Name",
                value: "Plaine (I7)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 80L,
                column: "Name",
                value: "Plaine (I8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 81L,
                column: "Name",
                value: "Plaine (I9)");

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 18, 22, 50, 31, 163, DateTimeKind.Local).AddTicks(9757));

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name", "PlayerId", "Type", "X", "Y", "Assets" },
                values: new object[,]
                {
                    { 2L, "Avant-Poste", 1L, "Building", 1, 1, "{\"Type\":{\"Avant-Poste\":null}}" },
                    { 3L, "Avant-Poste", 1L, "Building", 6, 6, "{\"Type\":{\"Avant-Poste\":null}}" },
                    { 4L, "Village", 1L, "Building", 2, 3, "{\"Type\":{\"Village\":null}}" },
                    { 5L, "Village", 1L, "Building", 3, 6, "{\"Type\":{\"Village\":null}}" },
                    { 6L, "Village", 1L, "Building", 5, 2, "{\"Type\":{\"Village\":null}}" },
                    { 7L, "Village", 1L, "Building", 6, 5, "{\"Type\":{\"Village\":null}}" },
                    { 22L, "Entrée", 5L, "Building", 8, 4, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 8L, "Entrée", 2L, "Building", 0, 3, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 9L, "Entrée", 2L, "Building", 0, 4, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 10L, "Entrée", 2L, "Building", 0, 5, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 11L, "Entrée", 2L, "Building", 0, 6, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 12L, "Entrée", 3L, "Building", 3, 0, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 13L, "Entrée", 3L, "Building", 4, 0, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 14L, "Entrée", 3L, "Building", 5, 0, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 15L, "Entrée", 3L, "Building", 6, 0, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 16L, "Entrée", 4L, "Building", 2, 8, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 17L, "Entrée", 4L, "Building", 3, 8, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 18L, "Entrée", 4L, "Building", 4, 8, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 19L, "Entrée", 4L, "Building", 5, 8, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 20L, "Entrée", 5L, "Building", 8, 2, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 21L, "Entrée", 5L, "Building", 8, 3, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 23L, "Entrée", 5L, "Building", 8, 5, "{\"Type\":{\"Entr\\u00E9e\":null}}" },
                    { 1L, "Forteresse", 1L, "Building", 4, 4, "{\"Type\":{\"Forteresse\":null}}" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Form",
                value: "[{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Manoeuvre sournoise : Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 2 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Form",
                value: "[{\"Label\":\"Case\",\"Type\":3,\"Description\":\"La case de renfort cibl\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Soutien militaire : D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\"},{\"Label\":\"Destination\",\"Type\":5,\"Description\":\"La destination de l\\u0027arm\\u00E9e\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\"},{\"Label\":\"Formation\",\"Type\":6,\"Description\":\"La formation \\u00E0 adopter\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"Discipline : D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire \\u00E0 espionner\"},{\"Label\":\"Augmentation\",\"Type\":1,\"Description\":\"R\\u00E9seau d\\u0027information : D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\"}]");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 10, 17, 16, 57, 26, 395, DateTimeKind.Local).AddTicks(7650));

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
                column: "Name",
                value: "Entrée (A3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Name",
                value: "Entrée (A4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Name",
                value: "Entrée (A5)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Name",
                value: "Entrée (A6)");

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
                column: "Name",
                value: "Avant-poste (B1)");

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
                column: "Name",
                value: "Village (C3)");

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
                column: "Name",
                value: "Entrée (C8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 28L,
                column: "Name",
                value: "Entrée (D0)");

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
                column: "Name",
                value: "Village (D6)");

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
                column: "Name",
                value: "Entrée (D8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 37L,
                column: "Name",
                value: "Entrée (E0)");

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
                column: "Name",
                value: "Forteresse (E4)");

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
                column: "Name",
                value: "Entrée (E8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 46L,
                column: "Name",
                value: "Entrée (F0)");

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
                column: "Name",
                value: "Village (F2)");

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
                column: "Name",
                value: "Entrée (F8)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 55L,
                column: "Name",
                value: "Entrée (G0)");

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
                column: "Name",
                value: "Village (G5)");

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
                column: "Name",
                value: "Avant-poste (H7)");

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
                column: "Name",
                value: "Entrée (I2)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 76L,
                column: "Name",
                value: "Entrée (I3)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 77L,
                column: "Name",
                value: "Entrée (I4)");

            migrationBuilder.UpdateData(
                table: "MapTiles",
                keyColumn: "Id",
                keyValue: 78L,
                column: "Name",
                value: "Entrée (I5)");

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
    }
}
