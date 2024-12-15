using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Men_Of_Varna.Data.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public string CustomerId { get; set; } = null!;

        [ForeignKey(nameof(CustomerId))]
        public IdentityUser Customer { get; set; } = null!;

        public List<Product> Products { get; set; } = new();

        [Required]
        public string ShippingAddress { get; set; } = null!;

        [Required]
        public string ShippingCity { get; set; } = null!;

        [Required]
        public string ShippingZip { get; set; } = null!;

        public string OrderStatus { get; set; } = "Pending";

        [NotMapped]
        public decimal OrderTotal => Products.Sum(p => p.Price);

        public List<OrderProduct> OrderProducts { get; set; } = new();
    }
}
