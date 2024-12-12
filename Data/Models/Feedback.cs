namespace Men_Of_Varna.Data.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SubmittedOn { get; set; }
        public int? EventId { get; set; }
        public Event Event { get; set; } = null!;
        public int? ProductId { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }

}
