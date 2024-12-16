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

            var adminUser = new IdentityUser
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
            builder.Entity<IdentityUser>().HasData(adminUser);
            builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { RoleId = "6d6c79e8-055f-4508-9102-b8345fc1ca6d", UserId = adminUser.Id });

            builder.Entity<Product>()
                   .Property(p => p.Price)
                   .HasColumnType("decimal(18,2)");

            builder.Entity<UserEvent>()
                   .HasKey(ue => new { ue.UserId, ue.EventId });

            builder.Entity<OrderProduct>()
                   .HasKey(op => new { op.OrderId, op.ProductId });

            builder.Entity<OrderProduct>()
                   .HasOne(op => op.Order)  
                   .WithMany(o => o.OrderProducts)  
                   .HasForeignKey(op => op.OrderId);

            builder.Entity<OrderProduct>()
                   .HasOne(op => op.Product)  
                   .WithMany(p => p.OrderProducts)  
                   .HasForeignKey(op => op.ProductId);

            builder.Entity<UserEvent>()
                   .HasOne(ue => ue.User)
                   .WithMany() 
                   .HasForeignKey(ue => ue.UserId);

            builder.Entity<UserEvent>()
                  .HasOne(ue => ue.Event)
                  .WithMany(e => e.UserEvents) 
                  .HasForeignKey(ue => ue.EventId);

            builder.Entity<Comment>()
                   .HasOne(c => c.Event)
                   .WithMany(e => e.Comments) // Ensure Event has a Comments collection
                   .HasForeignKey(c => c.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Beach Cleanup",
                    Description = "Join us to clean up the beautiful beaches of Varna.",
                    PictureUrl = "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=",
                    PublishedOn = new DateTime(2024, 1, 5),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 2,
                    Name = "Mountain Hike",
                    Description = "An adventurous hike through the scenic mountains.",
                    PictureUrl = "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg",
                    PublishedOn = new DateTime(2024, 12, 12),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = true
                },
                new Event
                {
                    Id = 3,
                    Name = "Community Yoga",
                    Description = "Relax and rejuvenate with a free yoga session for all.",
                    PictureUrl = "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg",
                    PublishedOn = new DateTime(2024, 3, 8),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 4,
                    Name = "Book Club Meeting",
                    Description = "Discussing 'The Way of the Superior Man' by David Deida.",
                    PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s",
                    PublishedOn = new DateTime(2024, 4, 1),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 6,
                    Name = "Beach Cleanup",
                    Description = "Join us to clean up the beautiful beaches of Varna.",
                    PictureUrl = "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=",
                    PublishedOn = new DateTime(2024, 1, 5),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 7,
                    Name = "Mountain Hike",
                    Description = "An adventurous hike through the scenic mountains.",
                    PictureUrl = "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg",
                    PublishedOn = new DateTime(2024, 12, 12),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = true
                },
                new Event
                {
                    Id = 8,
                    Name = "Community Yoga",
                    Description = "Relax and rejuvenate with a free yoga session for all.",
                    PictureUrl = "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg",
                    PublishedOn = new DateTime(2024, 3, 8),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 9,
                    Name = "Book Club Meeting",
                    Description = "Discussing 'The Way of the Superior Man' by David Deida.",
                    PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s",
                    PublishedOn = new DateTime(2024, 4, 1),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 10,
                    Name = "Beach Cleanup",
                    Description = "Join us to clean up the beautiful beaches of Varna.",
                    PictureUrl = "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=",
                    PublishedOn = new DateTime(2024, 1, 5),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 11,
                    Name = "Mountain Hike",
                    Description = "An adventurous hike through the scenic mountains.",
                    PictureUrl = "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg",
                    PublishedOn = new DateTime(2024, 12, 12),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = true
                },
                new Event
                {
                    Id = 12,
                    Name = "Community Yoga",
                    Description = "Relax and rejuvenate with a free yoga session for all.",
                    PictureUrl = "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg",
                    PublishedOn = new DateTime(2024, 3, 8),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
                new Event
                {
                    Id = 13,
                    Name = "Book Club Meeting",
                    Description = "Discussing 'The Way of the Superior Man' by David Deida.",
                    PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s",
                    PublishedOn = new DateTime(2024, 4, 1),
                    CreatedBy = "admin@menofvarna.com",
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
