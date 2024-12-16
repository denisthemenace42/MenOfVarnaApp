using Men_Of_Varna.Data.Models;

namespace Men_Of_Varna.Models.Events
{
    public class EventDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public DateTime PublishedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
        public bool IsUpcoming { get; set; }
        public int AttendeesCount { get; set; }
        public bool IsAttending { get; set; } 
        public List<CommentViewModel> Comments { get; set; } = new();
    }

    public class CommentViewModel
    {
        public string Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; } = null!;
    }

}
