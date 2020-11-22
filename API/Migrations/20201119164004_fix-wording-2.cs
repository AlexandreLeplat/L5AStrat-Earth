using Microsoft.EntityFrameworkCore.Migrations;

namespace HostApp.Migrations
{
    public partial class fixwording2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\",\"Parameter\":null,\"IsPredefinedOnMap\":true,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Destination\",\"Type\":\"Move\",\"Description\":\"La destination de l\\u0027arm\\u00E9e\",\"Parameter\":\"Arm\\u00E9e\",\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":true,\"MapDescription\":\"S\\u00E9lectionnez la destination sur la carte\"},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez 5 points de Strat\\u00E9gie pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort, un changement de formation ou un premier d\\u00E9placement.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Color", "Name" },
                values: new object[] { "lightgrey", "Neutre" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActionTypes",
                keyColumn: "Id",
                keyValue: 5L,
                column: "Form",
                value: "[{\"Label\":\"Arm\\u00E9e\",\"Type\":\"Army\",\"Description\":\"L\\u0027arm\\u00E9e \\u00E0 d\\u00E9placer\",\"Parameter\":null,\"IsPredefinedOnMap\":true,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null},{\"Label\":\"Destination\",\"Type\":\"Move\",\"Description\":\"La destination de l\\u0027arm\\u00E9e\",\"Parameter\":\"Arm\\u00E9e\",\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":true,\"MapDescription\":\"S\\u00E9lectionnez la destination sur la carte\"},{\"Label\":\"Augmentation\",\"Type\":\"Checkbox\",\"Description\":\"Ligne de ravitaillement : D\\u00E9pensez un point d\\u0027Influence pour d\\u00E9placer l\\u0027arm\\u00E9e apr\\u00E8s un renfort ou un changement de formation.\",\"Parameter\":null,\"IsPredefinedOnMap\":false,\"IsSelectedTileOnMap\":false,\"IsSelectableOnMap\":false,\"MapDescription\":null}]");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Color", "Name" },
                values: new object[] { null, "Admin" });
        }
    }
}
