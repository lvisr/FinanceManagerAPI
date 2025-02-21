using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabaseFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash", "Username" },
                values: new object[] { "jane@example.com", "SecurePass456", "Jane Smith" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { 1, "john@example.com", "Password123", "John Doe" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Email", "PasswordHash", "Username" },
                values: new object[] { "john@example.com", "Password123", "John Doe" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[] { 3, "jane@example.com", "SecurePass456", "Jane Smith" });
        }
    }
}
