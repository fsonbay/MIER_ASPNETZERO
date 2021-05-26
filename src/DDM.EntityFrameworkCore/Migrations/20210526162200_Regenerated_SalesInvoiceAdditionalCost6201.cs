using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Regenerated_SalesInvoiceAdditionalCost6201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LineAmount",
                table: "SalesInvoiceAdditionalCosts");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "SalesInvoiceAdditionalCosts");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SalesInvoiceAdditionalCosts",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SalesInvoiceAdditionalCosts",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalesInvoiceAdditionalCosts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SalesInvoiceAdditionalCosts");

            migrationBuilder.AddColumn<decimal>(
                name: "LineAmount",
                table: "SalesInvoiceAdditionalCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "SalesInvoiceAdditionalCosts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
