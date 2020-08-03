using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name", "Password", "Role" },
                values: new object[,]
                {
                    { 1L, "Admin", "$2a$11$0cCo4Ciq8/0QZszDtAkP.eK969i/yEeK0bLIU3Tr8Zrut/BxkT5wS", 0 },
                    { 2L, "Dragon", "$2a$11$1lokWFVxSB.CnBrMpKlFNOnKJm5w04ZCgEX4SBRyz83OYXU7XOqla", 0 },
                    { 3L, "Grue", "$2a$11$A8cdb6KPnCYxTdEz42jzZ.UOCNlF/9jMg/KuVf6Dm0DdoFUU2N1Bu", 0 },
                    { 4L, "Lion", "$2a$11$i3YhcWnD0DFn9mq/geDUD.XBqvWGqb1kn/7nJtPxFeX3vfxc3w8Ie", 0 },
                    { 5L, "Scorpion", "$2a$11$.B24rVXy6wHIdKC1uF49dOzem5wFa4nFbD3PumlzAaXDnmhP67T6O", 0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
