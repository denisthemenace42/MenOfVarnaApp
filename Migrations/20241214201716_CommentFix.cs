using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Men_Of_Varna.Migrations
{
    /// <inheritdoc />
    public partial class CommentFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac63d55a-6446-4129-8fc0-33ec92a4655e", "AQAAAAIAAYagAAAAECNcbM4j72MAe0sNZ96QjyIQj+Dvzq9YRnKy4vxJ2iQm22gUHOOSozzpY2RlXfleLQ==", "1e7f917a-8a95-4fde-a7e6-27c567f82b38" });

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1,
                column: "SubmittedOn",
                value: new DateTime(2024, 12, 14, 20, 17, 16, 4, DateTimeKind.Utc).AddTicks(6545));

            migrationBuilder.UpdateData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2,
                column: "SubmittedOn",
                value: new DateTime(2024, 12, 14, 20, 17, 16, 4, DateTimeKind.Utc).AddTicks(6547));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderDate",
                value: new DateTime(2024, 12, 14, 20, 17, 16, 4, DateTimeKind.Utc).AddTicks(6564));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderDate",
                value: new DateTime(2024, 12, 13, 20, 17, 16, 4, DateTimeKind.Utc).AddTicks(6566));

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "EventId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
