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

            builder.Entity<UserDestination>()
                   .HasKey(ud => new { ud.UserId, ud.DestinationId });

            builder.Entity<Destination>()
            .HasOne(d => d.Terrain)
            .WithMany(t => t.Destinations)
            .HasForeignKey(d => d.TerrainId);

            builder.Entity<UserEvent>()
        .HasKey(ue => new { ue.UserId, ue.EventId }); // Composite primary key

           

            builder.Entity<UserEvent>()
                .HasOne(ue => ue.Event)
                .WithMany(e => e.UserEvents)
                .HasForeignKey(ue => ue.EventId);

            builder.Entity<Terrain>()
                .HasData(
                    new Terrain { Id = 1, Name = "Mountain" },
                    new Terrain { Id = 2, Name = "Beach" },
                    new Terrain { Id = 3, Name = "Forest" },
                    new Terrain { Id = 4, Name = "Plain" },
                    new Terrain { Id = 5, Name = "Urban" },
                    new Terrain { Id = 6, Name = "Village" },
                    new Terrain { Id = 7, Name = "Cave" },
                    new Terrain { Id = 8, Name = "Canyon" }
                );

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



            builder.Entity<Destination>().HasData(
        new Destination
        {
            Id = 1,
            Name = "Rila Monastery",
            Description = "A stunning historical landmark nestled in the Rila Mountains.",
            ImageUrl = "https://img.etimg.com/thumb/msid-112831459,width-640,height-480,imgsize-2180890,resizemode-4/rila-monastery-bulgaria.jpg",
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            PublishedOn = DateTime.Now,
            TerrainId = 1,
            IsDeleted = false
        },
        new Destination
        {
            Id = 2,
            Name = "Durankulak Beach",
            Description = "The sand at Durankulak Beach showcases a pristine golden color, creating a beautiful contrast against the azure waters of the Black Sea.",
            ImageUrl = "https://travelplanner.ro/blog/wp-content/uploads/2023/01/durankulak-beach-1-850x550.jpg.webp",
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            PublishedOn = DateTime.Now,
            TerrainId = 2,
            IsDeleted = false
        },
        new Destination
        {
            Id = 3,
            Name = "Devil's Throat Cave",
            Description = "A mysterious cave located in the Rhodope Mountains.",
            ImageUrl = "https://detskotobnr.binar.bg/wp-content/uploads/2017/11/Diavolsko_garlo_17.jpg",
            PublisherId = "7699db7d-964f-4782-8209-d76562e0fece",
            PublishedOn = DateTime.Now,
            TerrainId = 7,
            IsDeleted = false
        });
    
        }
        

        public DbSet<Destination> Destinations { get; set; } 

        public DbSet<Terrain> Terrains { get; set; }

        public DbSet<UserDestination> UserDestinations { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
