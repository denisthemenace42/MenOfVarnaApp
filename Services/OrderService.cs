using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data;
using Men_Of_Varna.Models.Order;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
namespace Men_Of_Varna.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProductService productService;

        private const string CartSessionKey = "ShoppingCart";

        public OrderService(ApplicationDbContext dbContext, IProductService productService)
        {
            this.dbContext = dbContext;
            this.productService = productService;
        }

        public OrderViewModel GetCartFromSession(ISession session)
        {
            var value = session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(value))
            {
                return new OrderViewModel();
            }

            try
            {
                var cart = JsonConvert.DeserializeObject<OrderViewModel>(value);
                return cart ?? new OrderViewModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Error deserializing cart: " + ex.Message);
                return new OrderViewModel();
            }
        }

        public void SaveCartToSession(ISession session, OrderViewModel cart)
        {
            var value = JsonConvert.SerializeObject(cart);
            Console.WriteLine("🛒 Cart before saving: " + value); 
            session.SetString(CartSessionKey, value);
        }


        public async Task AddToCartAsync(ISession session, int productId, string productName, decimal price)
        {
            var cart = GetCartFromSession(session);
            var existingProduct = cart.Products.FirstOrDefault(p => p.Id == productId);
            var product = await productService.GetByIdAsync(productId);

            if (product == null)
            {
                throw new Exception("Product not found.");
            }

            if (existingProduct != null)
            {
                existingProduct.Quantity++;

                if (existingProduct.Quantity > product.StockQuantity)
                {
                    existingProduct.Quantity = product.StockQuantity;
                }
            }
            else
            {
                cart.Products.Add(new OrderProductViewModel
                {
                    Id = productId,
                    Name = product.Name, 
                    Price = product.Price, 
                    Quantity = 1, 
                    StockQuantity = product.StockQuantity 
                });
            }

            SaveCartToSession(session, cart);
        }


        public void RemoveFromCart(ISession session, int productId)
        {
            var cart = GetCartFromSession(session);
            cart.Products.RemoveAll(p => p.Id == productId);
            SaveCartToSession(session, cart);
        }

        public async Task CheckoutAsync(ISession session, string userId)
        {
            // Handle checkout logic (e.g., save the order to the database)
            // Here you can persist the order, send an email confirmation, etc.

            // Example of clearing the cart
            session.Remove(CartSessionKey);
            await Task.CompletedTask; 
        }

        public void UpdateQuantity(ISession session, int productId, int quantity)
        {
            var cart = GetCartFromSession(session);

            if (cart == null || cart.Products == null)
            {
                return;
            }

            var product = cart.Products.FirstOrDefault(p => p.Id == productId);

            if (product != null)
            {
                product.Quantity = quantity;
            }
          
            SaveCartToSession(session, cart);
        }


    }
}
