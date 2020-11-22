using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class fixwording : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 1 point de Gloire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 3 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 2L,
                column: "Form",
                value: "[{\"Label\":\"Cible\",\"Type\":\"Opponent\",\"Description\":\"L\\u0027adversaire recevant 1 point de Gloire\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Faveur politique : D\\u00E9pensez un point d\\u2019Influence pour gagner 4 points de Gloire suppl\\u00E9mentaires et ne cibler personne\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");
        }
    }
}
