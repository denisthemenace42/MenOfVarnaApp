using System.ComponentModel.DataAnnotations;

namespace Men_Of_Varna.Models.Order
{
    public class OrderViewModel
    {
        public List<OrderProductViewModel> Products { get; set; } = new();

        public decimal TotalAmount
        {
            get
            {
                return Products?.Sum(p => p.TotalPrice) ?? 0m;
            }
        }

        [Required(ErrorMessage = "Shipping address is required.")]
        [StringLength(200, ErrorMessage = "Shipping address cannot exceed 200 characters.")]
        public string ShippingAddress { get; set; } = null!;

        [Required(ErrorMessage = "Shipping city is required.")]
        [StringLength(100, ErrorMessage = "City name cannot exceed 100 characters.")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "City name can only contain letters and spaces.")]
        public string ShippingCity { get; set; } = null!;

        [Required(ErrorMessage = "Shipping ZIP code is required.")]
        [StringLength(10, ErrorMessage = "ZIP code cannot exceed 10 characters.")]
        [RegularExpression(@"^\d{4,10}$", ErrorMessage = "ZIP code must be between 4 and 10 digits.")]
        public string ShippingZip { get; set; } = null!;
    }

    public class OrderProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int StockQuantity { get; set; } 

        public decimal TotalPrice
        {
            get
            {
                return Price * Quantity;
            }
        }
    }
}
