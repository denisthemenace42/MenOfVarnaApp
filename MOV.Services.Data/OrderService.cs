using Microsoft.EntityFrameworkCore;
using MOV.Data;
using MOV.Services.Data.Interfaces;
using Newtonsoft.Json;
using MOV.Data.Models;
using MOV.ViewModels.Order;
using Microsoft.AspNetCore.Http;
namespace MOV.Services.Data
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IProductService productService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private const string CartSessionKey = "ShoppingCart";

        public OrderService(ApplicationDbContext dbContext, IProductService productService, IHttpContextAccessor httpContextAccessor)
        {
            this.dbContext = dbContext;
            this.productService = productService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task PlaceOrderAsync(Order order)
        {
            if (order.OrderProducts == null || !order.OrderProducts.Any())
            {
                throw new InvalidOperationException("Order must have at least one product.");
            }

            Console.WriteLine("Placing order...");
            Console.WriteLine($"Order has {order.OrderProducts.Count} products");

            foreach (var orderProduct in order.OrderProducts)
            {
                var productFromDb = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == orderProduct.ProductId);
                if (productFromDb == null)
                {
                    throw new InvalidOperationException($"Product with ID {orderProduct.ProductId} does not exist.");
                }

                orderProduct.Product = productFromDb;
                Console.WriteLine($"Product Linked: {orderProduct.Product.Name} - Quantity: {orderProduct.Quantity}");
            }

            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();
        }


        public async Task<List<Order>> GetUserOrdersAsync(string userId)
        {
            return await dbContext.Orders
                .Where(o => o.CustomerId == userId)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();
        }


        public async Task<Order?> GetOrderDetailsAsync(int orderId, string userId)
        {
            return await dbContext.Orders
                .Where(o => o.Id == orderId && o.CustomerId == userId)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int orderId)
        {
            return await dbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

        public async Task UpdateOrderStatusAsync(int orderId, string status)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null) throw new InvalidOperationException("Order not found.");

            order.OrderStatus = status;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);
            if (order == null) throw new InvalidOperationException("Order not found.");

            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
        }
    }
}
