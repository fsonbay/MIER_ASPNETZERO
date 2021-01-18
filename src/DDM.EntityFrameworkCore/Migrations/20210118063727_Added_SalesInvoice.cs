using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Added_SalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    DueDate = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Paid = table.Column<decimal>(nullable: false),
                    Outstanding = table.Column<decimal>(nullable: false),
                    SalesOrderId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoice_SalesOrder_SalesOrderId",
                        column: x => x.SalesOrderId,
                        principalTable: "SalesOrder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoice_SalesOrderId",
                table: "SalesInvoice",
                column: "SalesOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoice");
        }
    }
}
