using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class tileActions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_jsonActions",
                table: "MapTiles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Maps",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Form",
                value: "[{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Manoeuvre sournoise : Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 3 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Form",
                value: "[{\"Label\":\"Case\",\"Type\":\"EntryTile\",\"Description\":\"La case de renfort cibl\\u00E9e\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":true,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Formation\",\"Type\":\"Formation\",\"Description\":\"La formation \\u00E0 adopter\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Soutien militaire : D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\",\"Parameter\":null,\"IsPredefinedOnMap\":true,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Destination\",\"Type\":\"Move\",\"Description\":\"La destination de l\\u0027arm\\u00E9e\",\"Parameter\":\"Arm\\u00E9e\",\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":true,\"MapDescription\":\"S\\u00E9lectionnez la destination sur la carte\"},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\",\"Parameter\":null,\"IsPredefinedOnMap\":true,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Formation\",\"Type\":\"Formation\",\"Description\":\"La formation \\u00E0 adopter\",\"Parameter\":\"Arm\\u00E9e\",\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Discipline : D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire \\u00E0 espionner\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"R\\u00E9seau d\\u0027information : D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

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
                columns: new[] { "CreationDate", "Size" },
                values: new object[] { new DateTime(2020, 11, 5, 13, 51, 57, 342, DateTimeKind.Local).AddTicks(8852), 9 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_jsonActions",
                table: "MapTiles");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Maps");

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
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 3 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\",\"Parameter\":null}]");

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
                value: new DateTime(2020, 10, 29, 12, 26, 25, 868, DateTimeKind.Local).AddTicks(2141));

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 29, 0, 26, 25, 882, DateTimeKind.Local).AddTicks(3359));
        }
    }
}
