using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Men_Of_Varna.Migrations
{
    /// <inheritdoc />
    public partial class CommentSectionToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0c5d1fcd-1634-4431-ac89-bec857f5b5c2", "AQAAAAIAAYagAAAAEPlRfTPJdhspr0yfKJ83RGpd7NpStdbq2DJQXoQfQMxUyv4T2DqAiRfeZUQwTwEEcQ==", "f82f74a0-b465-4c5f-8f65-00440145b698" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubmittedOn",
                value: new DateTime(2024, 12, 14, 20, 10, 7, 538, DateTimeKind.Utc).AddTicks(923));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubmittedOn",
                value: new DateTime(2024, 12, 14, 20, 10, 7, 538, DateTimeKind.Utc).AddTicks(925));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 14, 20, 10, 7, 538, DateTimeKind.Utc).AddTicks(941));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 13, 20, 10, 7, 538, DateTimeKind.Utc).AddTicks(944));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Products_ProductId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_ProductId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Comments");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "025c6c6d-5c94-4936-94c5-8e1ce9e19ff4", "AQAAAAIAAYagAAAAELBO4z9p1niGBhfXKmuy3WWOPqVnlfnxDAtc9M17RcCNhpDZdE1pV10OgnfL+9GlgQ==", "0fb3f81d-019e-4bf7-b5b7-2ab6084b41c1" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubmittedOn",
                value: new DateTime(2024, 12, 14, 17, 19, 44, 678, DateTimeKind.Utc).AddTicks(2263));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubmittedOn",
                value: new DateTime(2024, 12, 14, 17, 19, 44, 678, DateTimeKind.Utc).AddTicks(2265));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 14, 17, 19, 44, 678, DateTimeKind.Utc).AddTicks(2286));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 13, 17, 19, 44, 678, DateTimeKind.Utc).AddTicks(2288));
        }
    }
}
