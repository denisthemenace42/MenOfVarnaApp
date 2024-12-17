using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOV.Data.Models
{
    public class OrderProduct
    {
        [Key]
        [Column(Order = 0)]
        public int OrderId { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; } = null!;

        [Key]
        [Column(Order = 1)]
        public int ProductId { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; } = null!;

        public int Quantity { get; set; }
    }
}
