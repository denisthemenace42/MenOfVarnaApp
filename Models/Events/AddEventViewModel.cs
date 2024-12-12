namespace Men_Of_Varna.Models.Events
{
    public class AddEventViewModel
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime PublishedOn { get; set; }
        public bool IsUpcoming { get; set; }
    }
}
