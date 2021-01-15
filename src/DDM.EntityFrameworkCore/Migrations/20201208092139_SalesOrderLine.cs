using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class SalesOrderLine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SalesOrderLineNames",
                table: "SalesOrder",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalesOrderLineNames",
                table: "SalesOrder");
        }
    }
}
