using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Models.Feedback
{
    // ViewModels/FeedbackViewModel.cs
    public class FeedbackViewModel
    {
        [Required]
        [StringLength(1000, ErrorMessage = "Feedback content cannot exceed 1000 characters.")]
        public string Content { get; set; } = null!;
       

        
    }



}
