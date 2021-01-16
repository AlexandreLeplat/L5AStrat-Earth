using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class wording : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Description",
                value: "Déployez une armée pour 4 points de Stratégie sur un point d'entrée, 5 sur un bâtiment militaire ou 6 sur un Village");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "Form" },
                values: new object[] { "Achetez un point d'Influence pour 4 points de Stratégie et 1 d'Infamie", "[{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Trafic d\\u0027influence : Vendez un point d\\u0027Influence et subissez un point d\\u0027Infamie pour acheter 4 points de Strat\\u00E9gie.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 4L,
                column: "Description",
                value: "Déployez une armée pour 5 points de Stratégie");

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Description", "Form" },
                values: new object[] { "Achetez un point d'Influence pour 5 points de Stratégie et 1 d'Infamie", "[{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Trafic d\\u0027influence : Vendez un point d\\u0027Influence et subissez un point d\\u0027Infamie pour acheter 5 points de Strat\\u00E9gie.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]" });
        }
    }
}
