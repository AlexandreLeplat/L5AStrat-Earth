using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class partieTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "Form" },
                values: new object[] { "Gagnez 1 point de Gloire et faites-en gagner 2 à un adversaire", "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null}]" });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 3 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CurrentPhase", "NextPhase", "PhaseLength", "Assets" },
                values: new object[] { 1, new DateTime(2020, 10, 28, 0, 15, 42, 117, DateTimeKind.Local).AddTicks(7743), 720, "{\"Prochain classement\":{\"Tour 3\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 27, 12, 15, 42, 132, DateTimeKind.Local).AddTicks(6475));

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Color", "Name", "Assets" },
                values: new object[] { "gold", "Matsu Kiperuganyu", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Doji Ujitsu", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Akodo Yama", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Color", "Name", "Assets" },
                values: new object[] { "green", "Togashi Atsu", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$11$BjZvEi2T4jXl.dONfcedm.Ll0sL6d226qBIxR.PT0G1LwLk6jJmQO");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "toutétékalculé", "$2a$11$yePEmRHE5RMu0lOXIxzt9.Z9sIda516qP3uICCmR2OVizTTDExVXi" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Elostirion ", "$2a$11$9nUwVLN8btVzPvLZAnUWDeFjMaUxzmfZH6TXjLFsPRMhXeeryXUOu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Akodostef", "$2a$11$jA8Nys/YYvA7EKVqNTgYzeUREtgvklcFtrY2kBEoD7VdG9IUAayyO" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Crabi", "$2a$11$H9XVGihkqHDssIMjGZW9NefJoocnv2y6NJ4PwVEIEosyp2QnjgQMy" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "Form" },
                values: new object[] { "Gagnez 2 points de Gloire et faites-en gagner 2 à un adversaire", "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null}]" });

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 3L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire subissant la m\\u00E9disance\",\"Parameter\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Scandale : D\\u00E9pensez un point d\\u2019Influence pour ajouter 2 points d\\u2019Infamie \\u00E0 votre cible et remplacer votre gain d\\u0027Infamie par un gain de Gloire.\",\"Parameter\":null}]");

            migrationBuilder.UpdateData(
                table: "Campaigns",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CurrentPhase", "NextPhase", "PhaseLength", "Assets" },
                values: new object[] { 2, new DateTime(2020, 10, 19, 22, 50, 31, 148, DateTimeKind.Local).AddTicks(8986), 1440, "{\"Classement\":{\"1er\":\"Doji Misao\",\"2\\u00E8me\":\"Kitsuki Hisao\",\"3\\u00E8me\":\"Yogo Rushi\",\"4\\u00E8me\":\"Ikoma Kiyoshi\"},\"Prochain classement\":{\"Tour 6\":\"\"}}" });

            migrationBuilder.UpdateData(
                table: "Maps",
                keyColumn: "Id",
                keyValue: 1L,
                column: "CreationDate",
                value: new DateTime(2020, 10, 18, 22, 50, 31, 163, DateTimeKind.Local).AddTicks(9757));

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ActionTypeId", "Comment", "OrdersSheetId", "Rank", "Status", "Parameters" },
                values: new object[,]
                {
                    { 1L, 1L, "+ 1 Strat", 1L, 0, 2, "{\"Augmentation\":\"false\"}" },
                    { 4L, 1L, "+ 1 Strat", 4L, 0, 2, "{\"Augmentation\":\"false\"}" },
                    { 3L, 4L, "+ 1 Infamie, Doji Misao +3 Infamie", 3L, 0, 2, "{\"Cible\":\"3\",\"Augmentation\":\"false\"}" },
                    { 2L, 2L, "+ 2 Gloire, Yogo Rushi + 2 Gloire", 2L, 0, 2, "{\"Cible\":\"5\",\"Augmentation\":\"false\"}" }
                });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Color", "Name", "Assets" },
                values: new object[] { "green", "Kitsuki Hisao", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"5\",\"Infamie\":\"1\"},\"Ressources\":{\"Strat\\u00E9gie\":\"3\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Doji Misao", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"7\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"2\",\"Influence\":\"1\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Name", "Assets" },
                values: new object[] { "Ikoma Kiyoshi", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"3\",\"Infamie\":\"0\"},\"Ressources\":{\"Strat\\u00E9gie\":\"5\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Color", "Name", "Assets" },
                values: new object[] { "red", "Yogo Rushi", "{\"Caract\\u00E9ristiques\":{\"Gloire\":\"4\",\"Infamie\":\"2\"},\"Ressources\":{\"Strat\\u00E9gie\":\"4\",\"Influence\":\"0\"}}" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "Password",
                value: "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Dragon", "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Grue", "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Lion", "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Name", "Password" },
                values: new object[] { "Scorpion", "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O" });
        }
    }
}
