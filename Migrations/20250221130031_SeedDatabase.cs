using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinanceManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "PasswordHash", "Username" },
                values: new object[,]
                {
                    { 1, "john@example.com", "AQAAAAIAAYagAAAAEANYsqKzwes8Qz/34KuFwSvDnrY+cqvtNfIDXBUx+uV83B4anF3GnnIIMiMi69Xvgw==", "John Doe" },
                    { 2, "jane@example.com", "AQAAAAIAAYagAAAAEPlJlMMWnEsmbjoSL8g2QVH1D5OLGH1imh9gkGcYotVfFFyQUNWLUt5/do3L0hgDPg==", "Jane Smith" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Balance", "Name", "Type", "UserId" },
                values: new object[,]
                {
                    { 1, 1500.00m, "Checking Account", 0, 1 },
                    { 2, 5000.00m, "Savings Account", 1, 1 },
                    { 3, 200.00m, "Cash", 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "AccountId", "Amount", "Date", "Description", "Type" },
                values: new object[,]
                {
                    { 1, 1, -100.00m, new DateTime(2025, 2, 21, 13, 0, 31, 52, DateTimeKind.Utc).AddTicks(9460), "Groceries", 1 },
                    { 2, 2, 500.00m, new DateTime(2025, 2, 21, 13, 0, 31, 52, DateTimeKind.Utc).AddTicks(9584), "Salary", 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Accounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
