using Microsoft.AspNetCore.Identity;

namespace Men_Of_Varna.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public IdentityUser User { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public List<Product> Products { get; set; } = new();
    }

}
