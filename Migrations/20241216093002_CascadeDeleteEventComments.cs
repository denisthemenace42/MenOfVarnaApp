using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Men_Of_Varna.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteEventComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Feedbacks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "OrderProducts",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

    
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c20faf94-1a63-4dc2-9b11-a76623157d40", "AQAAAAIAAYagAAAAEHvSJUbfIaM3EIS6bhM8vSjCpNE/GAzOXX8CvPXqMjRxXk1GN1RV9pJXfHKS/mT/LQ==", "b03e5398-af98-4721-9afc-0661663b7bed" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedBy",
                value: "admin@menofvarna.com");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedBy",
                value: "admin@menofvarna.com");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedBy",
                value: "admin@menofvarna.com");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedBy",
                value: "admin@menofvarna.com");

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedBy", "Description", "IsUpcoming", "Name", "PictureUrl", "PublishedOn" },
                values: new object[,]
                {
                    { 6, "admin@menofvarna.com", "Join us to clean up the beautiful beaches of Varna.", false, "Beach Cleanup", "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "admin@menofvarna.com", "An adventurous hike through the scenic mountains.", true, "Mountain Hike", "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "admin@menofvarna.com", "Relax and rejuvenate with a free yoga session for all.", false, "Community Yoga", "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "admin@menofvarna.com", "Discussing 'The Way of the Superior Man' by David Deida.", false, "Book Club Meeting", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "admin@menofvarna.com", "Join us to clean up the beautiful beaches of Varna.", false, "Beach Cleanup", "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "admin@menofvarna.com", "An adventurous hike through the scenic mountains.", true, "Mountain Hike", "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "admin@menofvarna.com", "Relax and rejuvenate with a free yoga session for all.", false, "Community Yoga", "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "admin@menofvarna.com", "Discussing 'The Way of the Superior Man' by David Deida.", false, "Book Club Meeting", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "6d6c79e8-055f-4508-9102-b8345fc1ca6d", "7699db7d-964f-4782-8209-d76562e0fece" });

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7699db7d-964f-4782-8209-d76562e0fece",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c457304f-2361-4025-a129-bb07daea499e", "AQAAAAIAAYagAAAAEOxZe56ES28S5LZFG21iNMaXLAzxcCYUmQIP8KyQhotJXmnGxxhjywLgmRrWTEY1Eg==", "5554b1ab-5755-41e0-9680-dac3bd5a62af" });

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedBy",
                value: "Men of Varna Admin");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedBy",
                value: "Denis Mehmed");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedBy",
                value: "Denis Mehmed");

            migrationBuilder.UpdateData(
                table: "Events",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedBy",
                value: "Men of Varna Admin");

            migrationBuilder.InsertData(
                table: "Feedbacks",
                columns: new[] { "Id", "Content", "SubmittedOn", "UserId" },
                values: new object[,]
                {
                    { 1, "This is an amazing product! I love the motivational quote and the comfort of the T-shirt.", new DateTime(2024, 12, 15, 16, 3, 15, 493, DateTimeKind.Utc).AddTicks(5477), "7699db7d-964f-4782-8209-d76562e0fece" },
                    { 2, "The canvas painting is beautiful! It adds a great vibe to my living room.", new DateTime(2024, 12, 15, 16, 3, 15, 493, DateTimeKind.Utc).AddTicks(5478), "7699db7d-964f-4782-8209-d76562e0fece" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "OrderDate", "OrderStatus", "ShippingAddress", "ShippingCity", "ShippingZip" },
                values: new object[,]
                {
                    { 1, "7699db7d-964f-4782-8209-d76562e0fece", new DateTime(2024, 12, 15, 16, 3, 15, 493, DateTimeKind.Utc).AddTicks(5495), "Pending", "123 Main St", "Varna", "9000" },
                    { 2, "7699db7d-964f-4782-8209-d76562e0fece", new DateTime(2024, 12, 14, 16, 3, 15, 493, DateTimeKind.Utc).AddTicks(5497), "Shipped", "456 Secondary St", "Varna", "9001" }
                });

            migrationBuilder.InsertData(
                table: "OrderProducts",
                columns: new[] { "OrderId", "ProductId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 1, 2, 0 },
                    { 2, 1, 0 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Events_EventId",
                table: "Comments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
