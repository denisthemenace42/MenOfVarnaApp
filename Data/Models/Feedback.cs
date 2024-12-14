using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Men_Of_Varna.Data.Models
{
    public class Feedback
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        [Required]
        [StringLength(1000, ErrorMessage = "Feedback content cannot exceed 1000 characters.")]
        public string Content { get; set; } = null!;

        public DateTime SubmittedOn { get; set; } = DateTime.UtcNow;

       
    }
}
