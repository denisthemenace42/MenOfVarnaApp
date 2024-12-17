using System.ComponentModel.DataAnnotations;

namespace MOV.Data.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string PictureUrl { get; set; } = null!;

        public int StockQuantity { get; set; }

        public string Category { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public List<Order> Orders { get; set; } = new();

        public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public List<Comment> Comments { get; set; } = new();
    }
}
