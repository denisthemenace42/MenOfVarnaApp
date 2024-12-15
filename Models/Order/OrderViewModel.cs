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
        public string ShippingAddress { get; set; } = null!;

        [Required(ErrorMessage = "Shipping city is required.")]
        public string ShippingCity { get; set; } = null!;

        [Required(ErrorMessage = "Shipping ZIP code is required.")]
        [StringLength(10, ErrorMessage = "ZIP code cannot exceed 10 characters.")]
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
