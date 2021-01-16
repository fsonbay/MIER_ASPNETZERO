using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Regenerated_ProductionStatus5370 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionStatuses",
                table: "ProductionStatuses");

            migrationBuilder.RenameTable(
                name: "ProductionStatuses",
                newName: "ProductionStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionStatus",
                table: "ProductionStatus",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductionStatus",
                table: "ProductionStatus");

            migrationBuilder.RenameTable(
                name: "ProductionStatus",
                newName: "ProductionStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductionStatuses",
                table: "ProductionStatuses",
                column: "Id");
        }
    }
}
