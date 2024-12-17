namespace Men_Of_Varna.Areas.Admin.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; } 
        public string CustomerEmail { get; set; } = string.Empty; 
        public DateTime OrderDate { get; set; } 
        public string OrderStatus { get; set; } = "Pending"; 
        public decimal TotalAmount { get; set; } 
    }
}
