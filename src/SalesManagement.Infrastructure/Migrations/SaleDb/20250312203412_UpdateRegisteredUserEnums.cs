using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesManagement.Infrastructure.Migrations.SaleDb
{
    /// <inheritdoc />
    public partial class UpdateRegisteredUserEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusTemp",
                table: "RegisteredUsers",
                nullable: false,
                defaultValue: 0 
            );

            migrationBuilder.AddColumn<int>(
                name: "RoleTemp",
                table: "RegisteredUsers",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.Sql(@"
        UPDATE ""RegisteredUsers"" 
        SET ""StatusTemp"" = 
            CASE 
                WHEN ""Status"" = 'Active' THEN 0
                WHEN ""Status"" = 'Inactive' THEN 1
                WHEN ""Status"" = 'Suspended' THEN 2
                ELSE 0
            END;
    ");

            migrationBuilder.Sql(@"
        UPDATE ""RegisteredUsers"" 
        SET ""RoleTemp"" = 
            CASE 
                WHEN ""Role"" = 'Customer' THEN 0
                WHEN ""Role"" = 'Manager' THEN 1
                WHEN ""Role"" = 'Admin' THEN 2
                ELSE 0
            END;
    ");

            migrationBuilder.DropColumn(name: "Status", table: "RegisteredUsers");
            migrationBuilder.DropColumn(name: "Role", table: "RegisteredUsers");

            migrationBuilder.RenameColumn(name: "StatusTemp", table: "RegisteredUsers", newName: "Status");
            migrationBuilder.RenameColumn(name: "RoleTemp", table: "RegisteredUsers", newName: "Role");
        }
    }
}