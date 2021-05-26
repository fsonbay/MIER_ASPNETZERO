using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Regenerated_SalesInvoicePayment2429 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesOrderId",
                table: "SalesInvoicePayment",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoicePayment_SalesOrderId",
                table: "SalesInvoicePayment",
                column: "SalesOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesInvoicePayment_SalesOrder_SalesOrderId",
                table: "SalesInvoicePayment",
                column: "SalesOrderId",
                principalTable: "SalesOrder",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesInvoicePayment_SalesOrder_SalesOrderId",
                table: "SalesInvoicePayment");

            migrationBuilder.DropIndex(
                name: "IX_SalesInvoicePayment_SalesOrderId",
                table: "SalesInvoicePayment");

            migrationBuilder.DropColumn(
                name: "SalesOrderId",
                table: "SalesInvoicePayment");
        }
    }
}
