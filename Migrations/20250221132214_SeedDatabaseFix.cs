using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceManagerAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 4, 4, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 4, 4, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "Password123");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "SecurePass456");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2025, 2, 21, 13, 0, 31, 52, DateTimeKind.Utc).AddTicks(9460));

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2025, 2, 21, 13, 0, 31, 52, DateTimeKind.Utc).AddTicks(9584));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEANYsqKzwes8Qz/34KuFwSvDnrY+cqvtNfIDXBUx+uV83B4anF3GnnIIMiMi69Xvgw==");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEPlJlMMWnEsmbjoSL8g2QVH1D5OLGH1imh9gkGcYotVfFFyQUNWLUt5/do3L0hgDPg==");
        }
    }
}
