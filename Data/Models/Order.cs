using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Men_Of_Varna.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; } = new();

        public string CustomerId { get; set; } = null!;

        [ForeignKey(nameof(CustomerId))]
        public IdentityUser Customer { get; set; } = null!;
    }

}
