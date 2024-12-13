namespace Men_Of_Varna.Models.Feedback
{
    public class FeedbackViewModel
    {
        public string Content { get; set; } = null!;
        public int? EventId { get; set; }  // Optional, based on your form
        public int? ProductId { get; set; }  // Optional, based on your form
    }
}
