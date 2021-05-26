using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Regenerated_SalesOrderLine8665 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Machine_MachineId",
                table: "SalesOrderLine");

            migrationBuilder.DropForeignKey(
                name: "FK_SalesOrderLine_Material_MaterialId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_MachineId",
                table: "SalesOrderLine");

            migrationBuilder.DropIndex(
                name: "IX_SalesOrderLine_MaterialId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "LineAmount",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "MachineId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "MaterialId",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "SalesOrderLine");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SalesOrderLine",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalesOrderLine");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SalesOrderLine");

            migrationBuilder.AddColumn<decimal>(
                name: "LineAmount",
                table: "SalesOrderLine",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "MachineId",
                table: "SalesOrderLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaterialId",
                table: "SalesOrderLine",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "SalesOrderLine",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_MachineId",
                table: "SalesOrderLine",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesOrderLine_MaterialId",
                table: "SalesOrderLine",
                column: "MaterialId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Machine_MachineId",
                table: "SalesOrderLine",
                column: "MachineId",
                principalTable: "Machine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SalesOrderLine_Material_MaterialId",
                table: "SalesOrderLine",
                column: "MaterialId",
                principalTable: "Material",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
