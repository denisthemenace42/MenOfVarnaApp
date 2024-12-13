
namespace Men_Of_Varna.Models.Events
{
    public class EditEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public DateTime PublishedOn { get; set; }
        public string CreatedBy { get; set; } = null!;
    }
}
