using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class addmaxorderscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxOrdersCount",
                table: "OrdersSheets",
                nullable: false,
                defaultValue: 0);

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
                value: new DateTime(2020, 8, 18, 22, 51, 30, 249, DateTimeKind.Local).AddTicks(9382));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ActionTypeId", "Comment", "OrdersSheetId", "Status", "Parameters" },
                values: new object[,]
                {
                    { 1L, 1L, "+ 1 Strat", 1L, 2, "{\"Augmentation\":\"false\"}" },
                    { 2L, 2L, "+ 2 Gloire, Yogo Rushi + 2 Gloire", 2L, 2, "{\"Cible\":\"5\",\"Augmentation\":\"false\"}" },
                    { 3L, 4L, "+ 1 Infamie, Doji Misao +3 Infamie", 3L, 2, "{\"Cible\":\"3\",\"Augmentation\":\"false\"}" },
                    { 4L, 1L, "+ 1 Strat", 4L, 2, "{\"Augmentation\":\"false\"}" }
                });

            migrationBuilder.UpdateData(
                table: "OrdersSheets",
                keyColumn: "Id",
                keyValue: 1L,
                column: "MaxOrdersCount",
                value: 5);

            migrationBuilder.UpdateData(
                table: "OrdersSheets",
                keyColumn: "Id",
                keyValue: 2L,
                column: "MaxOrdersCount",
                value: 5);

            migrationBuilder.UpdateData(
                table: "OrdersSheets",
                keyColumn: "Id",
                keyValue: 3L,
                column: "MaxOrdersCount",
                value: 5);

            migrationBuilder.UpdateData(
                table: "OrdersSheets",
                keyColumn: "Id",
                keyValue: 4L,
                column: "MaxOrdersCount",
                value: 5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DropColumn(
                name: "MaxOrdersCount",
                table: "OrdersSheets");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Form",
                value: "[{\"Label\":\"Augmentation (Manoeuvre sournoise)\",\"Type\":1,\"Description\":\"Gagnez 2 points d\\u0027Infamie pour gagner un point de Strat\\u00E9gie suppl\\u00E9mentaire\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\"},{\"Label\":\"Augmentation (Faveur politique)\",\"Type\":1,\"Description\":\"D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\"},{\"Label\":\"Augmentation (Scandale)\",\"Type\":1,\"Description\":\"D\\u00E9pensez un point d\\u2019Influence pour ajouter 2 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Form",
                value: "[{\"Label\":\"Case\",\"Type\":3,\"Description\":\"La case de renfort cibl\\u00E9e\"},{\"Label\":\"Augmentation (Soutien militaire)\",\"Type\":1,\"Description\":\"D\\u00E9pensez un point d\\u0027Influence \\u00E0 la place des points de Strat\\u00E9gie.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\"},{\"Label\":\"Destination\",\"Type\":5,\"Description\":\"La destination de l\\u0027arm\\u00E9e\"},{\"Label\":\"Augmentation (Ligne de ravitaillement)\",\"Type\":1,\"Description\":\"D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 6L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":4,\"Description\":\"L\\u0027arm\\u00E9e cibl\\u00E9e\"},{\"Label\":\"Formation\",\"Type\":6,\"Description\":\"La formation \\u00E0 adopter\"},{\"Label\":\"Augmentation (Discipline)\",\"Type\":1,\"Description\":\"D\\u00E9pensez un point de Strat\\u00E9gie pour changer de formation apr\\u00E8s un d\\u00E9placement.\"}]");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 7L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":2,\"Description\":\"L\\u0027adversaire \\u00E0 espionner\"},{\"Label\":\"Augmentation (R\\u00E9seau d\\u0027information)\",\"Type\":1,\"Description\":\"D\\u00E9pensez un point d\\u0027Influence pour annuler l\\u0027Infamie et cibler tous les adversaires.\"}]");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                column: "NextPhase",
                value: new DateTime(2020, 8, 14, 0, 30, 6, 585, DateTimeKind.Local).AddTicks(6039));
        }
    }
}
