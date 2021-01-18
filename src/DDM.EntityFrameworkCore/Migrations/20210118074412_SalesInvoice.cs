using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class SalesInvoice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DueDate",
                table: "SalesInvoice",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MarkForDelete",
                table: "SalesInvoice",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SalesInvoiceLineNames",
                table: "SalesInvoice",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarkForDelete",
                table: "SalesInvoice");

            migrationBuilder.DropColumn(
                name: "SalesInvoiceLineNames",
                table: "SalesInvoice");

            migrationBuilder.AlterColumn<string>(
                name: "DueDate",
                table: "SalesInvoice",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
