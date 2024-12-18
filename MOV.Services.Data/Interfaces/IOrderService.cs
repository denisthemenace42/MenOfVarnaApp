﻿using MOV.Data.Models;
using MOV.ViewModels.Order;
using Microsoft.AspNetCore.Http;

namespace MOV.Services.Data.Interfaces
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
        Task DeleteOrderAsync(int orderId);
        Task UpdateOrderStatusAsync(int orderId, string status);
        Task<Order?> GetOrderByIdAsync(int orderId);
        Task<List<Order>> GetAllOrdersAsync();
    }
}
