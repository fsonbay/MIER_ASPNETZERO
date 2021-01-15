using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Regenerated_CustomerCategory8703 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerCategories",
                table: "CustomerCategories");

            migrationBuilder.RenameTable(
                name: "CustomerCategories",
                newName: "CustomerCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerCategory",
                table: "CustomerCategory",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerCategory",
                table: "CustomerCategory");

            migrationBuilder.RenameTable(
                name: "CustomerCategory",
                newName: "CustomerCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerCategories",
                table: "CustomerCategories",
                column: "Id");
        }
    }
}
