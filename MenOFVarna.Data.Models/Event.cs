
namespace MenOFVarna.Data.Models
{
    public class Event
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Location { get; set; } = null!;

        public virtual ICollection<EventPicture> EventPictures { get; set; } = new HashSet<EventPicture>();

    }
}
