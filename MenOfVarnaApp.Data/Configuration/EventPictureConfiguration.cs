namespace MenOfVarnaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using MenOFVarna.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    internal class EventPictureConfiguration : IEntityTypeConfiguration<EventPicture>
    {
        public void Configure(EntityTypeBuilder<EventPicture> builder)
        {
            builder.HasKey(ep => new { ep.EventId, ep.PictureId });

            builder
                .HasOne(ep => ep.Picture)
                .WithMany( p => p.PictureEvenets)
                .HasForeignKey(ep => ep.PictureId);

            builder
                .HasOne(ep => ep.Event)
                .WithMany(p => p.EventPictures)
                .HasForeignKey(ep => ep.EventId);
        }
    }
}
