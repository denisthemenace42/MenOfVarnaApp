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


            builder.Entity<UserEvent>()
                   .HasKey(ue => new { ue.UserId, ue.EventId }); // Composite Key

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.User)
                .WithMany() // Adjust if there are navigation properties in `IdentityUser`
                .HasForeignKey(ue => ue.UserId);

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents) // Assuming Event has a collection of UserEvents
                .HasForeignKey(ue => ue.EventId);

            builder.Entity<Order>()
                   .HasOne(o => o.Customer)
                   .WithMany()
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = 1,
                    Name = "Beach Cleanup",
                    Description = "Join us to clean up the beautiful beaches of Varna.",
                    PictureUrl = "https://media.istockphoto.com/id/1435005446/photo/recyclers-cleaning-the-beach.jpg?s=612x612&w=0&k=20&c=92lBY2A3i0c32_1wd_tTulVcaW0crv8jItFucmS75qo=",
                    PublishedOn = new DateTime(2024, 1, 5),
                    CreatedBy = "Men of Varna Admin"
                },
                new Event
                {
                    Id = 2,
                    Name = "Mountain Hike",
                    Description = "An adventurous hike through the scenic mountains.",
                    PictureUrl = "https://www.c-and-a.com/image/upload/q_auto:good,ar_4:3,c_fill,g_auto:face,w_342/s/editorial/wandern-fernwandern/wandern-arten-text-media-header.jpg",
                    PublishedOn = new DateTime(2024, 2, 12),
                    CreatedBy = "Denis Mehmed"
                },
                new Event
                {
                    Id = 3,
                    Name = "Community Yoga",
                    Description = "Relax and rejuvenate with a free yoga session for all.",
                    PictureUrl = "https://us.images.westend61.de/0001286137pw/group-of-women-and-men-taking-part-in-a-yoga-class-on-a-hillside-MINF13084.jpg",
                    PublishedOn = new DateTime(2024, 3, 8),
                    CreatedBy = "Denis Mehmed"
                },
                new Event
                {
                    Id = 4,
                    Name = "Book Club Meeting",
                    Description = "Discussing 'The Way of the Superior Man' by David Deida.",
                    PictureUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROcs7-ie7L5V2_GIF8xzqredf5cHQunEC7GA&s",
                    PublishedOn = new DateTime(2024, 4, 1),
                    CreatedBy = "Men of Varna Admin"
                });


    
        }
        
        public DbSet<Event> Events { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<UserEvent> UserEvents { get; set; }
    }
}
