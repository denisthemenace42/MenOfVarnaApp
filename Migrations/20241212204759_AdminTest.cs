using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Men_Of_Varna.Migrations
{
    /// <inheritdoc />
    public partial class AdminTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe6de29b-c352-46bc-b3ee-5442731450cd", "AQAAAAIAAYagAAAAEJaspBBxzF6N2e7lh4QkfWmPQS3rVXN7q4hp6d3WyDDi60wobidhmXOSKgxm/ifFgA==", "a51f8c1d-9095-432a-9ea2-460dd6d9fe75" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 12, 22, 47, 58, 608, DateTimeKind.Local).AddTicks(7593));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 12, 22, 47, 58, 608, DateTimeKind.Local).AddTicks(7603));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 12, 22, 47, 58, 608, DateTimeKind.Local).AddTicks(7605));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ae69198-81dd-48e8-b415-7d2618a84136", "AQAAAAIAAYagAAAAEFtOqLoLuL30EVLtT/EWWKw/+LyFXAy17mBCEilnHmrPwaeGkA09X6fTJQlASInhzw==", "025a40d4-9268-459b-b27e-d89173ed355c" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 12, 21, 39, 59, 207, DateTimeKind.Local).AddTicks(3616));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 12, 21, 39, 59, 207, DateTimeKind.Local).AddTicks(3629));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 12, 21, 39, 59, 207, DateTimeKind.Local).AddTicks(3632));
        }
    }
}
