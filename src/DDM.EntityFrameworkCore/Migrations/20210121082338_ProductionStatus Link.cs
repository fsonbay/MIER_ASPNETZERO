using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class ProductionStatusLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SalesOrder_ProductionStatusId",
                table: "SalesOrder",
                column: "ProductionStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrder_ProductionStatus_ProductionStatusId",
                table: "SalesOrder",
                column: "ProductionStatusId",
                principalTable: "ProductionStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrder_ProductionStatus_ProductionStatusId",
                table: "SalesOrder");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrder_ProductionStatusId",
                table: "SalesOrder");
        }
    }
}
