using NuGet.Protocol.Core.Types;

namespace Men_Of_Varna.Data.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public DateTime PublishedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public List<Comment> Comments { get; set; } = new();
        public bool IsUpcoming { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }
}

