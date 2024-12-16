using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Models.Feedback
{
    // ViewModels/FeedbackViewModel.cs
    public class FeedbackViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        [Required(ErrorMessage = "Feedback description is required.")]
        [StringLength(500, ErrorMessage = "Feedback description cannot exceed 500 characters.")]
        public string Content { get; set; } = string.Empty;
        public DateTime SubmittedOn { get; set; }

    }
}
