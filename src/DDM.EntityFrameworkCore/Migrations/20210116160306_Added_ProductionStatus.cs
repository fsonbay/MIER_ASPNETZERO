using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Added_ProductionStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "SalesOrder");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Deadline",
                table: "SalesOrder",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "MarkForDelete",
                table: "SalesOrder",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ProcessedBySurabaya",
                table: "SalesOrder",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ProductionStatusId",
                table: "SalesOrder",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ProductionStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionStatuses", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductionStatuses");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "Deadline",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "MarkForDelete",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "ProcessedBySurabaya",
                table: "SalesOrder");

            migrationBuilder.DropColumn(
                name: "ProductionStatusId",
                table: "SalesOrder");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "SalesOrder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
