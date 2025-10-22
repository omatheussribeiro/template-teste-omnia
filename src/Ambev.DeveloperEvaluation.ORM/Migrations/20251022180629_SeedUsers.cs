using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "Username", "Password", "Phone", "Email", "Role", "Status", "CreatedAt", "UpdatedAt" },
            values: new object[]
            {
                "admin",
                "$2a$12$u9FSTREch8M2oM7brh7gIuwiWTr6bAGQmCGK4O7ztHZqweDJ5Pv/q",
                "11999999999",
                "admin@ambev.com",
                "Admin",
                "Active",
                DateTime.UtcNow,
                null
            });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
