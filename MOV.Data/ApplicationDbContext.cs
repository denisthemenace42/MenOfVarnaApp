using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MOV.Data.Models;
using System.Reflection.Emit;

namespace MOV.Data
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
            builder.Entity<IdentityRole>().HasData(
       new IdentityRole
       {
           Id = "7699db7d-964f-4782-8209-d76562e0fece",
           Name = "Admin",
           NormalizedName = "ADMIN"
       },
       new IdentityRole
       {
           Id = "2f78dfe9-a7a8-4d6b-8232-222deba79003",
           Name = "User",
           NormalizedName = "USER"
       }
   );

     
            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = "a1b2c3d4-e5f6-7g8h-9i10-jk11lm12nopq",
                    UserName = "admin@menofvarna.com",
                    NormalizedUserName = "ADMIN@MENOFVARNA.COM",
                    Email = "admin@menofvarna.com",
                    NormalizedEmail = "ADMIN@MENOFVARNA.COM",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEJWwRPdqaxqBB+0oupZDWQ01P4ahJbDlAC0hsI0HAEIaZ+peeDWdd4Bkrv3sIGWEMw==",
                    SecurityStamp = "645b6698-a38f-445c-9e92-8a263681ea98",
                    ConcurrencyStamp = "3893d9ca-382d-40d2-9eba-0e52809b7021"
                }
            );


            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    UserId = "a1b2c3d4-e5f6-7g8h-9i10-jk11lm12nopq",
                    RoleId = "7699db7d-964f-4782-8209-d76562e0fece"
                }
                );
            builder.Entity<Product>()
                   .HasMany(p => p.Comments)
                   .WithOne(c => c.Product)
                   .HasForeignKey(c => c.ProductId);

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
                   .HasOne(c => c.Product)
                   .WithMany(p => p.Comments)
                   .HasForeignKey(c => c.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Comment>()
                   .HasOne(c => c.Event)
                   .WithMany(e => e.Comments)
                   .HasForeignKey(c => c.EventId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Beach Cleanup",
                    Description = "Join us to clean up the beautiful beaches of Varna.",
                    PictureUrl = "https://www.signupgenius.com/cms/socialMediaImages/beach-clean-up-tips-ideas-facebook-1200x630.png",
                    PublishedOn = new DateTime(2024, 1, 5),
                    CreatedBy = "admin@menofvarna.com",
                    IsUpcoming = false
                },
    new Event
    {
        Id = 2,
        Name = "Mountain Hike Adventure",
        Description = "An adventurous hike through the scenic mountains.",
        PictureUrl = "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg",
        PublishedOn = new DateTime(2024, 12, 12),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 3,
        Name = "Community Yoga Session",
        Description = "Relax and rejuvenate with a free yoga session for all.",
        PictureUrl = "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg",
        PublishedOn = new DateTime(2024, 3, 8),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = false
    },
    new Event
    {
        Id = 4,
        Name = "Book Club: The Way of the Superior Man",
        Description = "Discussing 'The Way of the Superior Man' by David Deida.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS4eQ8mWoCR27QphNLMQOXl11DjLmpPN3sA-Q&s",
        PublishedOn = new DateTime(2024, 4, 1),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = false
    },
    new Event
    {
        Id = 5,
        Name = "Cold Exposure Training",
        Description = "Boost your resilience with cold exposure training in the open sea.",
        PictureUrl = "https://images.squarespace-cdn.com/content/v1/5e08057bcf041b5662c92ed6/02f7390a-bf32-4357-8676-97974d4b55e2/004b4a82-4286-46f0-bf88-7bd006428699.JPG",
        PublishedOn = new DateTime(2024, 10, 15),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 6,
        Name = "Sunrise Meditation",
        Description = "Start your day with a calming sunrise meditation by the beach.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR9iUsU5VgGYMUGq56kn9o2RFZqsUmhdgg9LQ&s",
        PublishedOn = new DateTime(2024, 6, 20),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 7,
        Name = "Obstacle Course Challenge",
        Description = "Compete in a fun and challenging obstacle course with friends.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR8T4C_FOiufpKQlyBWZyJDdJT_6x2Yj1TOjg&s",
        PublishedOn = new DateTime(2024, 9, 2),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 8,
        Name = "Night Hike Adventure",
        Description = "Explore the trails at night with only a headlamp to guide you.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS6oZeeEU4XGDbzASDq0PdKevZpF44d7cFULg&s",
        PublishedOn = new DateTime(2024, 11, 18),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 9,
        Name = "Breathwork & Ice Bath",
        Description = "Discover the power of breathwork and experience a rejuvenating ice bath.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRAP-T1HItPZ2uLketuhnetCmcTcEP-pqIChA&s",
        PublishedOn = new DateTime(2024, 8, 15),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 10,
        Name = "Strength Training Bootcamp",
        Description = "Join us for a strength training bootcamp led by top trainers.",
        PictureUrl = "https://tunturi.org/Blogs/2021-08/bootcamp-full-body-workout.jpg",
        PublishedOn = new DateTime(2024, 2, 25),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = false
    },
    new Event
    {
        Id = 11,
        Name = "Trail Running Meetup",
        Description = "Run together through scenic trails and meet other trail enthusiasts.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQHw1Uz7ZT_SGawOAqvTW8Woa023zkVxTrXaw&s",
        PublishedOn = new DateTime(2024, 5, 3),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = false
    },
    new Event
    {
        Id = 12,
        Name = "Guided Meditation for Focus",
        Description = "Enhance your focus and mental clarity with this guided meditation.",
        PictureUrl = "https://cdn.prod.website-files.com/639ba154cb866a1c9aba97cd/63b4414fac8f1cb8e7b617b6_How%20to%20lead%20a%20guided%20meditation%20(1).jpg",
        PublishedOn = new DateTime(2024, 7, 25),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 13,
        Name = "Powerlifting Workshop",
        Description = "Learn the essentials of powerlifting in this hands-on workshop.",
        PictureUrl = "https://images.squarespace-cdn.com/content/v1/614d2cd0f5f2ae630442f65e/1634702587473-6AYH847J99WQQE2FWBCO/socials-06517.jpg",
        PublishedOn = new DateTime(2024, 8, 5),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    },
    new Event
    {
        Id = 14,
        Name = "HIIT Workout Blitz",
        Description = "High-Intensity Interval Training to get your heart pumping.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQwa9Vpc1nkZ315vuX_S-Dhlp393Oe5kYWz9g&s",
        PublishedOn = new DateTime(2024, 3, 28),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = false
    },
    new Event
    {
        Id = 15,
        Name = "Sunset Beach Workout",
        Description = "End the day with a full-body workout as the sun sets on the beach.",
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTRgutcIfWcLdyTW0V0bxFfkccYoZo5eg7Mdw&s",
        PublishedOn = new DateTime(2024, 7, 1),
        CreatedBy = "admin@menofvarna.com",
        IsUpcoming = true
    });


            builder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Motivational T-Shirt",
                    Description = "Elevate your style and mindset with this premium 100% cotton T-shirt featuring an inspiring motivational quote. Crafted for comfort and durability, this soft, breathable tee is perfect for daily wear, whether you're hitting the gym, running errands, or just lounging. The classic fit and bold design make it a standout piece in any wardrobe.",
                    Price = 25.99m,
                    PictureUrl = "https://i.etsystatic.com/12381665/r/il/4e7f53/4106896277/il_570xN.4106896277_o801.jpg",
                    StockQuantity = 100,
                    Category = "Clothes",
                    IsActive = true
                },
    new Product
    {
        Id = 2,
        Name = "Premium Hoodie",
        Description = "Stay warm, comfortable, and stylish with our premium hoodie. Made from high-quality fleece, this hoodie offers warmth and durability. It features a motivational quote on the back to inspire you every day. The perfect choice for workouts, casual wear, or lounging at home.",
        Price = 45.99m,
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSrdgzRbBNgX9lsQvKAuLk2SfuE5Csaa3ALlQ&s",
        StockQuantity = 50,
        Category = "Clothes",
        IsActive = true
    },
    new Product
    {
        Id = 3,
        Name = "Performance Joggers",
        Description = "Run, walk, or lounge in style with these lightweight, breathable joggers. Designed with a perfect balance of comfort and performance, they are ideal for workouts, morning jogs, or a day of relaxation. Features deep side pockets and a sleek, modern design.",
        Price = 39.99m,
        PictureUrl = "https://www.gymreapers.com/cdn/shop/products/performance-jogger-black-front.jpg?v=1725463127&width=1024",
        StockQuantity = 70,
        Category = "Clothes",
        IsActive = true
    },

    // 🟢 Art
    new Product
    {
        Id = 4,
        Name = "Lion Motivational Canvas",
        Description = "Transform your space with this stunning canvas painting featuring a powerful motivational quote and a fierce lion. This bold, high-quality canvas is perfect for home gyms, offices, or living spaces, adding a sense of power and strength to any environment.",
        Price = 109.99m,
        PictureUrl = "https://chrisfabregasfineartprints.com/cdn/shop/products/chris-fabregas-fine-art-photography-motivational-canvas-12-x-18-no-frame-lion-motivational-canvas-inspirational-wall-decor-wall-art-print-40115752108341.jpg?v=1671702904&width=1875",
        StockQuantity = 10,
        Category = "Art",
        IsActive = true
    },
    new Product
    {
        Id = 5,
        Name = "Abstract Motivational Poster",
        Description = "This abstract motivational poster is designed to inspire greatness and positivity. With its vibrant colors and minimalist aesthetic, it fits perfectly into modern offices, gyms, and creative spaces. Stay focused and motivated every time you glance at this bold design.",
        Price = 49.99m,
        PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSJqK0iMet-t--TGAsOGVf6oHjRjCqJSX5wRQ&s",
        StockQuantity = 20,
        Category = "Art",
        IsActive = true
    },
    new Product
    {
        Id = 6,
        Name = "Nature-Inspired Wall Art",
        Description = "Bring the tranquility of nature into your home with this beautiful nature-inspired wall art. Perfect for those who seek calm, relaxation, and an escape from the busy world. The artwork features a lush forest landscape to promote peace and serenity.",
        Price = 89.99m,
        PictureUrl = "https://www.artvistagallery.com/cdn/shop/files/1_f7d6ccb9-5e4b-4db3-a5aa-95f926942eea.jpg?v=1724489788&width=2183",
        StockQuantity = 15,
        Category = "Art",
        IsActive = true
    },

    new Product
    {
        Id = 7,
        Name = "The Way of the Superior Man",
        Description = "A transformative book for those seeking to master their purpose, relationships, and personal growth. 'The Way of the Superior Man' is a premium resource for anyone committed to self-improvement. A must-read for cultivating discipline, strength, and clarity in life.",
        Price = 15.99m,
        PictureUrl = "https://s3.studylib.net/store/data/025388611_1-10e08a326b93674972383ecf70de1438-768x994.png",
        StockQuantity = 25,
        Category = "Books",
        IsActive = true
    },
    new Product
    {
        Id = 8,
        Name = "Can't Hurt Me",
        Description = "David Goggins' best-selling autobiography, 'Can't Hurt Me,' is a must-read for those seeking mental toughness and discipline. This book shares powerful lessons about perseverance and embracing discomfort for personal growth.",
        Price = 18.99m,
        PictureUrl = "https://m.media-amazon.com/images/I/81gTRv2HXrL.jpg",
        StockQuantity = 40,
        Category = "Books",
        IsActive = true
    },
    new Product
    {
        Id = 9,
        Name = "Atomic Habits",
        Description = "James Clear's 'Atomic Habits' reveals how small changes lead to big transformations. Learn to break bad habits, build new ones, and achieve extraordinary results with practical, science-backed strategies.",
        Price = 21.99m,
        PictureUrl = "https://m.media-amazon.com/images/I/91bYsX41DVL.jpg",
        StockQuantity = 30,
        Category = "Books",
        IsActive = true
    },
    new Product
    {
        Id = 10,
        Name = "Deep Work",
        Description = "Master the art of focus and eliminate distractions with Cal Newport's 'Deep Work.' Learn to work deeply, maximize productivity, and create meaningful, high-impact work in an era of constant distractions.",
        Price = 14.99m,
        PictureUrl = "https://m.media-amazon.com/images/I/71pqZChaJkL.jpg",
        StockQuantity = 45,
        Category = "Books",
        IsActive = true
    },
    new Product
    {
        Id = 11,
        Name = "The 5AM Club",
        Description = "Robin Sharma's 'The 5AM Club' teaches the power of waking up early and creating a morning routine that maximizes productivity and well-being. Join the 5AM revolution and change your life.",
        Price = 19.99m,
        PictureUrl = "https://cdn.ozone.bg/media/catalog/product/cache/1/image/400x498/a4e40ebdc3e371adff845072e1c73f37/c/8/c5e46bd4370a2f2519a162267f59eaaa/the-5-am-club-30.jpg",
        StockQuantity = 50,
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
