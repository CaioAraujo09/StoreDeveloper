using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagement.Infrastructure.Migrations.SaleDb
{
    /// <inheritdoc />
    public partial class AddRelationsSalesToUsersRegisters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "SaleDate",
                table: "Sales",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Sales",
                newName: "RegisteredUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_CustomerId",
                table: "Sales",
                newName: "IX_Sales_RegisteredUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_RegisteredUsers_RegisteredUserId",
                table: "Sales",
                column: "RegisteredUserId",
                principalTable: "RegisteredUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_RegisteredUsers_RegisteredUserId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "RegisteredUserId",
                table: "Sales",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Sales",
                newName: "SaleDate");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_RegisteredUserId",
                table: "Sales",
                newName: "IX_Sales_CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Customers_CustomerId",
                table: "Sales",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
