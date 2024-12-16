namespace Men_Of_Varna.Areas.Admin.ViewModels
{
    public class OrderDetailViewModel
    {
        public int Id { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; } = "Pending";
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public string ShippingZip { get; set; } = string.Empty;

        public List<OrderProductViewModel> Products { get; set; } = new List<OrderProductViewModel>();

        public decimal TotalAmount
        {
            get
            {
                return Products.Sum(p => p.Price * p.Quantity);
            }
        }
    }

    public class OrderProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PictureUrl { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

