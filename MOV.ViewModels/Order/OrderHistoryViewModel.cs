namespace MOV.ViewModels.Order
{
    public class OrderHistoryViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public string ShippingAddress { get; set; } = null!;
        public string ShippingCity { get; set; } = null!;
        public string ShippingZip { get; set; } = null!;
        public decimal OrderTotal { get; set; }
        public string OrderStatus { get; set; } = null!;
    }
}
