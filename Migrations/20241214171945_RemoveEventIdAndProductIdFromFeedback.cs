using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Men_Of_Varna.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEventIdAndProductIdFromFeedback : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Events_EventId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_Products_ProductId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_EventId",
                table: "Feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_ProductId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Feedbacks");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Feedbacks",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "32885117-3f81-46ec-acca-84f61eab5975", "AQAAAAIAAYagAAAAEHRxwK9pL86avA9QqRYwjKPTpPznyTmG4Q3r4nA1W+dkVv6pBEMcAIVZDmu2sXSRMg==", "bb29865f-3567-47a7-bfed-beaaa8309012" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EventId", "ProductId", "SubmittedOn" },
                values: new object[] { null, 1, new DateTime(2024, 12, 13, 17, 28, 52, 177, DateTimeKind.Utc).AddTicks(6699) });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EventId", "ProductId", "SubmittedOn" },
                values: new object[] { null, 2, new DateTime(2024, 12, 13, 17, 28, 52, 177, DateTimeKind.Utc).AddTicks(6701) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 13, 17, 28, 52, 177, DateTimeKind.Utc).AddTicks(6720));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 12, 17, 28, 52, 177, DateTimeKind.Utc).AddTicks(6722));

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EventId",
                table: "Feedbacks",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ProductId",
                table: "Feedbacks",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Events_EventId",
                table: "Feedbacks",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_Products_ProductId",
                table: "Feedbacks",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
