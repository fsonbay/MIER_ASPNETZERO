using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Added_SalesInvoicePayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesInvoicePayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    SalesInvoiceId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoicePayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoicePayment_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesInvoicePayment_SalesInvoice_SalesInvoiceId",
                        column: x => x.SalesInvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoicePayment_PaymentMethodId",
                table: "SalesInvoicePayment",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoicePayment_SalesInvoiceId",
                table: "SalesInvoicePayment",
                column: "SalesInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoicePayment");
        }
    }
}
