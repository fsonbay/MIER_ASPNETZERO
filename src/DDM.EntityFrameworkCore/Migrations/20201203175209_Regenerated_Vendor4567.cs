using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DDM.Migrations
{
    public partial class Regenerated_Vendor4567 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Vendors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Vendors",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VendorCategoryId",
                table: "Vendors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_VendorCategoryId",
                table: "Vendors",
                column: "VendorCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vendors_VendorCategory_VendorCategoryId",
                table: "Vendors",
                column: "VendorCategoryId",
                principalTable: "VendorCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vendors_VendorCategory_VendorCategoryId",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_VendorCategoryId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "VendorCategoryId",
                table: "Vendors");
        }
    }
}
