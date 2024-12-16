using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Men_Of_Varna.Migrations
{
    /// <inheritdoc />
    public partial class InitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsUpcoming = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    SubmittedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserEvents",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: false),
                    JoinedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEvents", x => new { x.UserId, x.EventId });
                    table.ForeignKey(
                        name: "FK_UserEvents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEvents_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    OrdersId = table.Column<int>(type: "int", nullable: false),
                    ProductsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => new { x.OrdersId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_OrderProduct_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderProducts_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2f78dfe9-a7a8-4d6b-8232-222deba79003", null, "User", "USER" },
                    { "7699db7d-964f-4782-8209-d76562e0fece", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "a1b2c3d4-e5f6-7g8h-9i10-jk11lm12nopq", 0, "3893d9ca-382d-40d2-9eba-0e52809b7021", "admin@menofvarna.com", true, false, null, "ADMIN@MENOFVARNA.COM", "ADMIN@MENOFVARNA.COM", "AQAAAAIAAYagAAAAEJWwRPdqaxqBB+0oupZDWQ01P4ahJbDlAC0hsI0HAEIaZ+peeDWdd4Bkrv3sIGWEMw==", null, false, "645b6698-a38f-445c-9e92-8a263681ea98", false, "admin@menofvarna.com" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "Id", "CreatedBy", "Description", "IsUpcoming", "Name", "PictureUrl", "PublishedOn" },
                values: new object[,]
                {
                    { 1, "admin@menofvarna.com", "Join us to clean up the beautiful beaches of Varna.", false, "Beach Cleanup", "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg", new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "admin@menofvarna.com", "An adventurous hike through the scenic mountains.", true, "Mountain Hike Adventure", "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg", new DateTime(2024, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "admin@menofvarna.com", "Relax and rejuvenate with a free yoga session for all.", false, "Community Yoga Session", "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg", new DateTime(2024, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "admin@menofvarna.com", "Discussing 'The Way of the Superior Man' by David Deida.", false, "Book Club: The Way of the Superior Man", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4eQ8mWoCR27QphNLMQOXl11DjLmpPN3sA-Q&s", new DateTime(2024, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, "admin@menofvarna.com", "Boost your resilience with cold exposure training in the open sea.", true, "Cold Exposure Training", "https://images.squarespace-cdn.com/content/v1/5e08057bcf041b5662c92ed6/02f7390a-bf32-4357-8676-97974d4b55e2/004b4a82-4286-46f0-bf88-7bd006428699.JPG", new DateTime(2024, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, "admin@menofvarna.com", "Start your day with a calming sunrise meditation by the beach.", true, "Sunrise Meditation", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9iUsU5VgGYMUGq56kn9o2RFZqsUmhdgg9LQ&s", new DateTime(2024, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, "admin@menofvarna.com", "Compete in a fun and challenging obstacle course with friends.", true, "Obstacle Course Challenge", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8T4C_FOiufpKQlyBWZyJDdJT_6x2Yj1TOjg&s", new DateTime(2024, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, "admin@menofvarna.com", "Explore the trails at night with only a headlamp to guide you.", true, "Night Hike Adventure", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS6oZeeEU4XGDbzASDq0PdKevZpF44d7cFULg&s", new DateTime(2024, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, "admin@menofvarna.com", "Discover the power of breathwork and experience a rejuvenating ice bath.", true, "Breathwork & Ice Bath", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAP-T1HItPZ2uLketuhnetCmcTcEP-pqIChA&s", new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, "admin@menofvarna.com", "Join us for a strength training bootcamp led by top trainers.", false, "Strength Training Bootcamp", "https://tunturi.org/Blogs/2021-08/bootcamp-full-body-workout.jpg", new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, "admin@menofvarna.com", "Run together through scenic trails and meet other trail enthusiasts.", false, "Trail Running Meetup", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHw1Uz7ZT_SGawOAqvTW8Woa023zkVxTrXaw&s", new DateTime(2024, 5, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, "admin@menofvarna.com", "Enhance your focus and mental clarity with this guided meditation.", true, "Guided Meditation for Focus", "https://cdn.prod.website-files.com/639ba154cb866a1c9aba97cd/63b4414fac8f1cb8e7b617b6_How%20to%20lead%20a%20guided%20meditation%20(1).jpg", new DateTime(2024, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, "admin@menofvarna.com", "Learn the essentials of powerlifting in this hands-on workshop.", true, "Powerlifting Workshop", "https://images.squarespace-cdn.com/content/v1/614d2cd0f5f2ae630442f65e/1634702587473-6AYH847J99WQQE2FWBCO/socials-06517.jpg", new DateTime(2024, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, "admin@menofvarna.com", "High-Intensity Interval Training to get your heart pumping.", false, "HIIT Workout Blitz", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQwa9Vpc1nkZ315vuX_S-Dhlp393Oe5kYWz9g&s", new DateTime(2024, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, "admin@menofvarna.com", "End the day with a full-body workout as the sun sets on the beach.", true, "Sunset Beach Workout", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRgutcIfWcLdyTW0V0bxFfkccYoZo5eg7Mdw&s", new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Category", "Description", "IsActive", "Name", "PictureUrl", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, "Clothes", "Elevate your style and mindset with this premium 100% cotton T-shirt featuring an inspiring motivational quote. Crafted for comfort and durability, this soft, breathable tee is perfect for daily wear, whether you're hitting the gym, running errands, or just lounging. The classic fit and bold design make it a standout piece in any wardrobe.", true, "Motivational T-Shirt", "https://i.etsystatic.com/12381665/r/il/4e7f53/4106896277/il_570xN.4106896277_o801.jpg", 25.99m, 100 },
                    { 2, "Clothes", "Stay warm, comfortable, and stylish with our premium hoodie. Made from high-quality fleece, this hoodie offers warmth and durability. It features a motivational quote on the back to inspire you every day. The perfect choice for workouts, casual wear, or lounging at home.", true, "Premium Hoodie", "https://i.etsystatic.com/13717122/r/il/59140b/2175825962/il_570xN.2175825962_8vhu.jpg", 45.99m, 50 },
                    { 3, "Clothes", "Run, walk, or lounge in style with these lightweight, breathable joggers. Designed with a perfect balance of comfort and performance, they are ideal for workouts, morning jogs, or a day of relaxation. Features deep side pockets and a sleek, modern design.", true, "Performance Joggers", "https://i.etsystatic.com/18951319/r/il/0b2cb8/1944940075/il_570xN.1944940075_rqjl.jpg", 39.99m, 70 },
                    { 4, "Art", "Transform your space with this stunning canvas painting featuring a powerful motivational quote and a fierce lion. This bold, high-quality canvas is perfect for home gyms, offices, or living spaces, adding a sense of power and strength to any environment.", true, "Lion Motivational Canvas", "https://chrisfabregasfineartprints.com/cdn/shop/products/chris-fabregas-fine-art-photography-motivational-canvas-12-x-18-no-frame-lion-motivational-canvas-inspirational-wall-decor-wall-art-print-40115752108341.jpg?v=1671702904&width=1875", 109.99m, 10 },
                    { 5, "Art", "This abstract motivational poster is designed to inspire greatness and positivity. With its vibrant colors and minimalist aesthetic, it fits perfectly into modern offices, gyms, and creative spaces. Stay focused and motivated every time you glance at this bold design.", true, "Abstract Motivational Poster", "https://images.unsplash.com/photo-1615297021074-14c6497f9c45", 49.99m, 20 },
                    { 6, "Art", "Bring the tranquility of nature into your home with this beautiful nature-inspired wall art. Perfect for those who seek calm, relaxation, and an escape from the busy world. The artwork features a lush forest landscape to promote peace and serenity.", true, "Nature-Inspired Wall Art", "https://images.unsplash.com/photo-1600271946896-9611cf3a491c", 89.99m, 15 },
                    { 7, "Books", "A transformative book for those seeking to master their purpose, relationships, and personal growth. 'The Way of the Superior Man' is a premium resource for anyone committed to self-improvement. A must-read for cultivating discipline, strength, and clarity in life.", true, "The Way of the Superior Man", "https://covers.openlibrary.org/b/id/7387836-L.jpg", 15.99m, 25 },
                    { 8, "Books", "David Goggins' best-selling autobiography, 'Can't Hurt Me,' is a must-read for those seeking mental toughness and discipline. This book shares powerful lessons about perseverance and embracing discomfort for personal growth.", true, "Can't Hurt Me", "https://m.media-amazon.com/images/I/61O45C5qASL.jpg", 18.99m, 40 },
                    { 9, "Books", "James Clear's 'Atomic Habits' reveals how small changes lead to big transformations. Learn to break bad habits, build new ones, and achieve extraordinary results with practical, science-backed strategies.", true, "Atomic Habits", "https://m.media-amazon.com/images/I/91bYsX41DVL.jpg", 21.99m, 30 },
                    { 10, "Books", "Master the art of focus and eliminate distractions with Cal Newport's 'Deep Work.' Learn to work deeply, maximize productivity, and create meaningful, high-impact work in an era of constant distractions.", true, "Deep Work", "https://m.media-amazon.com/images/I/81OJ1ZGQhyL.jpg", 14.99m, 45 },
                    { 11, "Books", "Robin Sharma's 'The 5AM Club' teaches the power of waking up early and creating a morning routine that maximizes productivity and well-being. Join the 5AM revolution and change your life.", true, "The 5AM Club", "https://m.media-amazon.com/images/I/71uOJo2p+oL.jpg", 19.99m, 50 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7699db7d-964f-4782-8209-d76562e0fece", "a1b2c3d4-e5f6-7g8h-9i10-jk11lm12nopq" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EventId",
                table: "Comments",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProductId",
                table: "Comments",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductsId",
                table: "OrderProduct",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductId",
                table: "OrderProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_UserEvents_EventId",
                table: "UserEvents",
                column: "EventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "OrderProducts");

            migrationBuilder.DropTable(
                name: "UserEvents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
