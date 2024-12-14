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



    }
}
