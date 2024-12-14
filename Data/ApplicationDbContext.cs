using Men_Of_Varna.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Men_Of_Varna.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var defaultUser = new IdentityUser
            {
                Id = "7699db7d-964f-4782-8209-d76562e0fece",
                UserName = "admin@menofvarna.com",
                NormalizedUserName = "ADMIN@MENOFVARNA.COM",
                Email = "admin@menofvarna.com",
                NormalizedEmail = "ADMIN@MENOFVARNA.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(
                    new IdentityUser { UserName = "admin@menofvarna.com" },
                    "320513")
            };
            builder.Entity<IdentityUser>().HasData(defaultUser);

            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<UserEvent>()
                   .HasKey(ue => new { ue.UserId, ue.EventId });

            builder.Entity<OrderProduct>()
    .HasKey(op => new { op.OrderId, op.ProductId });

            builder.Entity<OrderProduct>()
    .HasOne(op => op.Order)  // A Product can have many Orders through OrderProduct
    .WithMany(o => o.OrderProducts)  // Orders will have a collection of OrderProducts
    .HasForeignKey(op => op.OrderId);

            builder.Entity<OrderProduct>()
    .HasOne(op => op.Product)  // A Product can have many Orders through OrderProduct
    .WithMany(p => p.OrderProducts)  // Products will have a collection of OrderProducts
    .HasForeignKey(op => op.ProductId);

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany() 
                .HasForeignKey(ue => ue.UserId);

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents) 
                .HasForeignKey(ue => ue.EventId);


            builder.Entity<OrderProduct>().HasData(
    new OrderProduct { OrderId = 1, ProductId = 1 },
    new OrderProduct { OrderId = 1, ProductId = 2 },
    new OrderProduct { OrderId = 2, ProductId = 1 }
);


            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Beach Cleanup",
                    Description = "Join us to clean up the beautiful beaches of Varna.",
                    PictureUrl = "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=",
                    PublishedOn = new DateTime(2024, 1, 5),
                    CreatedBy = "Men of Varna Admin",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 2,
                    Name = "Mountain Hike",
                    Description = "An adventurous hike through the scenic mountains.",
                    PictureUrl = "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg",
                    PublishedOn = new DateTime(2024, 12, 12),
                    CreatedBy = "Denis Mehmed",
                    IsUpcoming = true
                },
                new Event
                {
                    Id = 3,
                    Name = "Community Yoga",
                    Description = "Relax and rejuvenate with a free yoga session for all.",
                    PictureUrl = "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg",
                    PublishedOn = new DateTime(2024, 3, 8),
                    CreatedBy = "Denis Mehmed",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 4,
                    Name = "Book Club Meeting",
                    Description = "Discussing 'The Way of the Superior Man' by David Deida.",
                    PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s",
                    PublishedOn = new DateTime(2024, 4, 1),
                    CreatedBy = "Men of Varna Admin",
                    IsUpcoming = false
                });


            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "T-Shirt",
                    Description = "Elevate your style and mindset with this premium 100% cotton T-shirt featuring an inspiring motivational quote. Crafted for comfort and durability, this soft, breathable tee is perfect for daily wear, whether you're hitting the gym, running errands, or just lounging. The classic fit and bold design make it a standout piece in any wardrobe. Stay motivated, confident, and stylish with this high-quality T-shirt, designed to empower you every day. Ideal for those who believe in the power of positive thinking and self-motivation.",
                    Price = 25.99m,
                    PictureUrl = "https://i.etsystatic.com/12381665/r/il/4e7f53/4106896277/il_570xN.4106896277_o801.jpg",
                    StockQuantity = 100,
                    Category = "Clothes",
                    IsActive = true
                },
                new Product
                {
                    Id = 2,
                    Name = "Canvas Painting",
                    Description = "Transform your space with this stunning canvas painting featuring a powerful motivational quote. Crafted with high-quality materials, this artwork adds both elegance and inspiration to any room. Whether displayed in your office, living room, or home gym, it serves as a constant reminder to stay focused, driven, and positive. With its timeless design and vibrant colors, this canvas painting is not only an affordable way to enhance your décor but also a daily source of motivation. Perfect for anyone looking to elevate their environment and mindset.",
                    Price = 109.99m,
                    PictureUrl = "https://chrisfabregasfineartprints.com/cdn/shop/products/chris-fabregas-fine-art-photography-motivational-canvas-12-x-18-no-frame-lion-motivational-canvas-inspirational-wall-decor-wall-art-print-40115752108341.jpg?v=1671702904&width=1875",
                    StockQuantity = 10,
                    Category = "Art",
                    IsActive = true
                },
                new Product
                {
                    Id = 3,
                    Name = "The way of the superior - Man",
                    Description = "A transformative book for those seeking to master their purpose, relationships, and personal growth. \"The Way of the Superior Man\" is a premium resource for professionals and anyone committed to self-improvement. A must-read for cultivating discipline, strength, and clarity in all areas of life.",
                    Price = 15.99m,
                    PictureUrl = "https://covers.openlibrary.org/b/id/7387836-L.jpg",
                    StockQuantity = 25,
                    Category = "Books",
                    IsActive = true
                }
            );

            builder.Entity<Feedback>().HasData(
        new Feedback
        {
            Id = 1,
            UserId = "7699db7d-964f-4782-8209-d76562e0fece", 
            Content = "This is an amazing product! I love the motivational quote and the comfort of the T-shirt.",
            SubmittedOn = DateTime.UtcNow,
           
        },
        new Feedback
        {
            Id = 2,
            UserId = "7699db7d-964f-4782-8209-d76562e0fece", 
            Content = "The canvas painting is beautiful! It adds a great vibe to my living room.",
            SubmittedOn = DateTime.UtcNow,
           
        }
    );
            builder.Entity<Order>().HasData(
        new Order
        {
            Id = 1,
            OrderDate = DateTime.UtcNow,
            CustomerId = "7699db7d-964f-4782-8209-d76562e0fece", 
            ShippingAddress = "123 Main St",
            ShippingCity = "Varna",
            ShippingZip = "9000",
            OrderStatus = "Pending"
        },
        new Order
        {
            Id = 2,
            OrderDate = DateTime.UtcNow.AddDays(-1),
            CustomerId = "7699db7d-964f-4782-8209-d76562e0fece", 
            ShippingAddress = "456 Secondary St",
            ShippingCity = "Varna",
            ShippingZip = "9001",
            OrderStatus = "Shipped"
        }
    );


        }
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<UserEvent> UserEvents { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
