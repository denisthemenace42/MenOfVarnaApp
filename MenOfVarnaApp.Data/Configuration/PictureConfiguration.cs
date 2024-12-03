namespace MenOfVarnaApp.Data.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using MenOFVarna.Data.Models;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using static MenOFVarna.Common.EntityValidationConstants.Picture;
    public class PictureConfiguration : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            
            // Fluent API
           builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
    .IsRequired();

            builder
                .Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(TitleMaxLength);

            builder
                .Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(DescriptionMaxLength);

            builder
                .Property(p => p.Author)
                .IsRequired()
                .HasMaxLength(AuthorMaxLength);

            builder.HasData(this.SeedPictures());
        }

        private List<Picture> SeedPictures()
        {
            List<Picture> pictures = new List<Picture>
    {
        new Picture()
        {
            Id = Guid.NewGuid(),
            Title = "Motivation",
            Author = "Denis",
            Description = "Join us"
        },
        new Picture()
        {
            Id = Guid.NewGuid(),
            Title = "Inspiration",
            Author = "Men of Varna",
            Description = "Lead by example"
        }
    };

            return pictures;
        }

    }

}
