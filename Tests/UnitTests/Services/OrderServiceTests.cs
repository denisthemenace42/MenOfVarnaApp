using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using MOV.Services;
using MOV.Data;
using MOV.Data.Models;
using MOV.ViewModels.Order;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using static MockDbSetHelper;
using MOV.Services.Data.Interfaces;
using MOV.Services.Data;
namespace Tests.UnitTests.Services
{
    public class OrderServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly OrderService _orderService;
        private readonly Mock<IProductService> _productServiceMock;
        private readonly Mock<ISession> _sessionMock;

        public OrderServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            SeedData(_dbContext);

            _productServiceMock = new Mock<IProductService>();
            _sessionMock = new Mock<ISession>();

            _orderService = new OrderService(_dbContext, _productServiceMock.Object, new HttpContextAccessor { HttpContext = new DefaultHttpContext { Session = _sessionMock.Object } });
        }

        private void SeedData(ApplicationDbContext dbContext)
        {
            var products = new List<Product>
{
    new Product
    {
        Id = 111,
        Name = "Product 1",
        Price = 100,
        StockQuantity = 10,
        Category = "Electronics",
        Description = "A sample product description for Product 1",
        PictureUrl = "https://example.com/image1.jpg"
    },
    new Product
    {
        Id = 222,
        Name = "Product 2",
        Price = 200,
        StockQuantity = 5,
        Category = "Appliances",
        Description = "A sample product description for Product 2",
        PictureUrl = "https://example.com/image2.jpg"
    }
};

            dbContext.Products.AddRange(products);

            var orders = new List<Order>
{
    new Order
    {
        Id = 1,
        CustomerId = "user1",
        OrderStatus = "Pending",
        ShippingAddress = "123 Main St",
        ShippingCity = "New York",
        ShippingZip = "10001",
        OrderProducts = new List<OrderProduct>
        {
            new OrderProduct { ProductId = 1, Quantity = 1 }
        }
    }
};
            dbContext.Orders.AddRange(orders);
            dbContext.SaveChanges();
        }

        [Fact]
        public void RemoveFromCart_ShouldRemoveProductFromCart()
        {
            var cart = new OrderViewModel
            {
                Products = new List<OrderProductViewModel>
        {
            new OrderProductViewModel { Id = 1, Name = "Product 1", Price = 100, Quantity = 1 },
            new OrderProductViewModel { Id = 2, Name = "Product 2", Price = 200, Quantity = 1 }
        }
            };

            var serializedCart = Newtonsoft.Json.JsonConvert.SerializeObject(cart);

            var httpContext = new DefaultHttpContext();
            var session = new MockHttpSession();
            session.SetString("ShoppingCart", serializedCart);
            httpContext.Session = session;

            _orderService.RemoveFromCart(httpContext.Session, 1);

            var updatedCart = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderViewModel>(session.GetString("ShoppingCart"));
            Assert.NotNull(updatedCart);
            Assert.Single(updatedCart.Products); 
            Assert.Equal(2, updatedCart.Products[0].Id); 
        }


        [Fact]
        public void UpdateQuantity_ShouldUpdateProductQuantityInCart()
        {
            var cart = new OrderViewModel
            {
                Products = new List<OrderProductViewModel>
        {
            new OrderProductViewModel { Id = 1, Name = "Product 1", Price = 100, Quantity = 1 }
        }
            };

            var serializedCart = Newtonsoft.Json.JsonConvert.SerializeObject(cart);
            var session = new MockHttpSession();
            session.SetString("ShoppingCart", serializedCart);

            var httpContext = new DefaultHttpContext
            {
                Session = session
            };

            var orderService = new OrderService(null, null, new HttpContextAccessor { HttpContext = httpContext });
            orderService.UpdateQuantity(httpContext.Session, 1, 5);

            var updatedCart = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderViewModel>(session.GetString("ShoppingCart"));
            Assert.NotNull(updatedCart);
            Assert.Equal(5, updatedCart.Products[0].Quantity);
        }


        [Fact]
        public async Task CheckoutAsync_ShouldClearCartFromSession()
        {
            await _orderService.CheckoutAsync(_sessionMock.Object, "user1");

            _sessionMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task PlaceOrderAsync_ShouldAddOrderToDatabase()
        {
            var order = new Order
            {
                CustomerId = "user1",
                OrderStatus = "Pending",
                ShippingAddress = "123 Main St",
                ShippingCity = "New York",
                ShippingZip = "10001",
                OrderProducts = new List<OrderProduct>
        {
            new OrderProduct { ProductId = 1, Quantity = 2 }
        }
            };

            await _orderService.PlaceOrderAsync(order);

            var orderInDb = await _dbContext.Orders.FirstOrDefaultAsync(o => o.CustomerId == "user1" && o.Id != 1);
            Assert.NotNull(orderInDb);
            Assert.Equal(1, orderInDb.OrderProducts.Count);
            Assert.Equal("123 Main St", orderInDb.ShippingAddress);
            Assert.Equal("New York", orderInDb.ShippingCity);
            Assert.Equal("10001", orderInDb.ShippingZip);
        }

        [Fact]
        public async Task GetUserOrdersAsync_ShouldReturnOrdersForUser()
        {
            var orders = await _orderService.GetUserOrdersAsync("user1");
            Assert.NotNull(orders);
            Assert.Equal(1, orders.Count);
            Assert.Equal("Pending", orders[0].OrderStatus);
            Assert.Equal("123 Main St", orders[0].ShippingAddress);
        }
    }
}

