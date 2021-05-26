using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Added_SalesInvoiceAdditionalCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SalesInvoiceAdditionalCosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<decimal>(nullable: false),
                    LineAmount = table.Column<decimal>(nullable: false),
                    UnitPrice = table.Column<decimal>(nullable: false),
                    SalesInvoiceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoiceAdditionalCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalesInvoiceAdditionalCosts_SalesInvoice_SalesInvoiceId",
                        column: x => x.SalesInvoiceId,
                        principalTable: "SalesInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoiceAdditionalCosts_SalesInvoiceId",
                table: "SalesInvoiceAdditionalCosts",
                column: "SalesInvoiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoiceAdditionalCosts");
        }
    }
}
