namespace Men_Of_Varna.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; } // Unique order ID
        public string CustomerEmail { get; set; } = string.Empty; // Email of the customer
        public DateTime OrderDate { get; set; } // When the order was placed
        public string OrderStatus { get; set; } = "Pending"; // Current status (Pending, Shipped, Completed, etc.)
        public decimal TotalAmount { get; set; } // Total price of the order
    }
}
