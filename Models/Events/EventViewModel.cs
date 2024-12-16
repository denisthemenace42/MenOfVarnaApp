using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Models.Events
{
    public class EventViewModel
    {
        [Required(ErrorMessage = "Event ID is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required.")]
        [StringLength(100, ErrorMessage = "Event name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL for the image.")]
        public string ImageUrl { get; set; } = null!;

        [Required(ErrorMessage = "Event description is required.")]
        [StringLength(500, ErrorMessage = "Event description cannot exceed 500 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "The publish date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Please enter a valid date.")]
        [Range(typeof(DateTime), "1/1/2020", "12/31/2099", ErrorMessage = "Publish date must be between 2020 and 2099.")]
        public DateTime PublishedOn { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Attendees count cannot be negative.")]
        public int AttendeesCount { get; set; }

        [Required(ErrorMessage = "Please specify if the current user is the publisher of the event.")]
        public bool IsPublisher { get; set; }

        [Required(ErrorMessage = "Please specify if the current user is attending the event.")]
        public bool IsAttending { get; set; }

        [Required(ErrorMessage = "Please specify if the event is upcoming.")]
        public bool IsUpcoming { get; set; }
    }

}
