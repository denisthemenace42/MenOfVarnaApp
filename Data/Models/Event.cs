using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Men_Of_Varna.Data.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The event name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000, ErrorMessage = "The event description cannot exceed 1000 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        [Url(ErrorMessage = "The picture URL must be a valid URL.")]
        public string PictureUrl { get; set; } = null!;

        [Required]
        public DateTime PublishedOn { get; set; } = DateTime.UtcNow; 

        [Required]
        [StringLength(50, ErrorMessage = "The creator's name cannot exceed 50 characters.")]
        public string CreatedBy { get; set; } = null!;

        public List<Comment> Comments { get; set; } = new();

        [Required]
        public bool IsUpcoming { get; set; } 
        public ICollection<UserEvent> UserEvents { get; set; } = new List<UserEvent>();
    }
}
