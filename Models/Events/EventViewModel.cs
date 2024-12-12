namespace Men_Of_Varna.Models.Events
{
    public class EventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PublishedOn { get; set; }
        public int AttendeesCount { get; set; }
        public bool IsPublisher { get; set; }
        public bool IsAttending { get; set; }
    }

}
