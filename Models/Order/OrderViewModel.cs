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
