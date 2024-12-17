using System.ComponentModel.DataAnnotations;

namespace MOV.ViewModels.Store
{
    public class EditProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Image URL is required.")]
        [Url(ErrorMessage = "Invalid URL format.")]
        public string PictureUrl { get; set; } = null!;

        [Required(ErrorMessage = "Stock quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be zero or a positive number.")]
        public int StockQuantity { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
