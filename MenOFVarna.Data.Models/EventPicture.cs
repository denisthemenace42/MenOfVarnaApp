
namespace MenOFVarna.Data.Models
{
    public class EventPicture
    {
        public Guid EventId { get; set; }

        public virtual Event Event { get; set; } = null!;

        public Guid PictureId { get; set; }
        
        public virtual Picture Picture { get; set; } = null!;

    }
}
