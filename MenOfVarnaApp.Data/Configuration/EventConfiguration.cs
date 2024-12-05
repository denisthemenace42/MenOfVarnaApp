namespace MenOfVarnaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using MenOFVarna.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .Property(p => p.Location)
                .IsRequired()
                .HasMaxLength(250);

            builder
                .Property(p => p.Date)
                .IsRequired();
            builder.HasData(this.GenerateEvents);

        }

        private IEnumerable<Event> GenerateEvents()
        {
            IEnumerable<Event> events = new List<Event>();
            {
                new Event
                {
                    Title = "The Begining",
                    Description = "Our first meetup",
                    Location = "Varna - Sea Garden",
                    Date = new DateTime(05 / 12 / 2024)
                };
                new Event
                {
                    Title = "Group Workout",
                    Description = "Running 20km ",
                    Location = "Varna - Levski",
                    Date = new DateTime(07 / 12 / 2024)
                };

                return events;
            }
        }
    }
}
