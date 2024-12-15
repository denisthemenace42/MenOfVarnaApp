using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Order;

namespace Men_Of_Varna.Contracts
{
    public interface IOrderService
    {
        OrderViewModel GetCartFromSession(ISession session);
        void SaveCartToSession(ISession session, OrderViewModel cart);
        Task AddToCartAsync(ISession session, int productId, string productName, decimal price);
        void RemoveFromCart(ISession session, int productId);
        Task CheckoutAsync(ISession session, string userId);
        void UpdateQuantity(ISession session, int productId, int quantity);
        Task PlaceOrderAsync(Order order);
        Task<Order?> GetOrderDetailsAsync(int orderId, string userId);
        Task<List<Order>> GetUserOrdersAsync(string userId);
    }
}
