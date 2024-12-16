using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Models.Events
{
    public class AddEventViewModel
    {
        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Event description is required.")]
        [StringLength(500, ErrorMessage = "Event description cannot exceed 500 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "The publish date is required.")]
        public DateTime PublishedOn { get; set; }

        public bool IsUpcoming { get; set; }
    }
}
