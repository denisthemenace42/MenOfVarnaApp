namespace Men_Of_Varna.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
    }

}