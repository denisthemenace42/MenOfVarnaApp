using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Horizons.Migrations
{
    /// <inheritdoc />
    public partial class Test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "34dc6889-072e-4edd-b847-5576ab36e490", "AQAAAAIAAYagAAAAEE3uUwzCImogq+M65uLJwxjo4OxOJLMz046+p3eGcGB6NLCU/LxskyRDWj1fo5KBcQ==", "58cab18e-9b3a-470f-b696-e7ee2892dc69" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 10, 13, 54, 19, 626, DateTimeKind.Local).AddTicks(9676));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 10, 13, 54, 19, 626, DateTimeKind.Local).AddTicks(9687));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 10, 13, 54, 19, 626, DateTimeKind.Local).AddTicks(9689));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "348c73e0-c33c-47a9-86fc-084f7d1ccefb", "AQAAAAIAAYagAAAAEDaWFbH5YRGdQ0/KnhKZf/9cVcKCEFd/Z3erCm1i9b1sfCgnbe6wC5h7g/4vuTJK3w==", "c7ae285b-adbc-45d9-a90f-5dc57ca3c2ab" });

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 1,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 10, 9, 25, 51, 951, DateTimeKind.Local).AddTicks(6066));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 2,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 10, 9, 25, 51, 951, DateTimeKind.Local).AddTicks(6077));

            migrationBuilder.UpdateData(
                table: "Destinations",
                keyColumn: "Id",
                keyValue: 3,
                column: "PublishedOn",
                value: new DateTime(2024, 12, 10, 9, 25, 51, 951, DateTimeKind.Local).AddTicks(6079));
        }
    }
}
