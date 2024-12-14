using System;
using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [StringLength(1000, ErrorMessage = "Content cannot exceed 1000 characters.")]
        public string Content { get; set; } = null!;

        [Required(ErrorMessage = "Author is required.")]
        [StringLength(100, ErrorMessage = "Author name cannot exceed 100 characters.")]
        public string Author { get; set; } = null!;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 


        public int? EventId { get; set; }

        public Event? Event { get; set; } = null!;

        
        public int? ProductId { get; set; }

        public Product? Product { get; set; } = null!;
    }
}
