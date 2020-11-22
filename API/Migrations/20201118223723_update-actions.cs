using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class updateactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "Form" },
                values: new object[] { "Gagnez 1 point de Gloire et faites-en gagner 1 à un adversaire", "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 1 point de Gloire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]" });

            migrationBuilder.InsertData(
                table: "ActionTypes",
                columns: new[] { "Id", "Description", "GameId", "Label", "Form" },
                values: new object[] { 8L, "Achetez un point d'Influence pour 5 points de Stratégie et 1 d'Infamie", 1L, "Commerce", "[{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Trafic d\\u0027influence : Vendez un point d\\u0027Influence et subissez un point d\\u0027Infamie pour acheter 5 points de Strat\\u00E9gie.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Description", "Form" },
                values: new object[] { "Gagnez 1 point de Gloire et faites-en gagner 2 à un adversaire", "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 2 points de Gloire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]" });
        }
    }
}
