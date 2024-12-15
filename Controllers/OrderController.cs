﻿using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Order;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly UserManager<IdentityUser> userManager;

        public OrderController(IOrderService orderService, UserManager<IdentityUser> userManager, IProductService productService)
        {
            this.orderService = orderService;
            this.userManager = userManager;
            this.productService = productService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = orderService.GetCartFromSession(HttpContext.Session);
            return View(cart);
        }

        // POST: Order/AddToCart
        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await productService.GetByIdAsync(id); 

            if (product == null)
            {
                return NotFound("Product not found.");
            }

            await orderService.AddToCartAsync(HttpContext.Session, id, product.Name, product.Price);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            orderService.RemoveFromCart(HttpContext.Session, productId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuantity([FromBody] ProductQuantityUpdateRequest request)
        {
            if (request == null)
            {
                return Json(new { success = false, message = "Invalid request." });
            }

            if (request.ProductId <= 0)
            {
                return Json(new { success = false, message = "Invalid Product ID." });
            }

            var product = await productService.GetByIdAsync(request.ProductId);

            if (product == null)
            {
                return Json(new { success = false, message = "Product not found." });
            }

            orderService.UpdateQuantity(HttpContext.Session, request.ProductId, request.Quantity);

            var cart = orderService.GetCartFromSession(HttpContext.Session);
            var updatedProduct = cart.Products.FirstOrDefault(p => p.Id == request.ProductId);

            if (updatedProduct == null)
            {
                return Json(new { success = false, message = "Product not found in cart." });
            }

            var updatedTotalPrice = updatedProduct.TotalPrice.ToString("C");
            var totalCartAmount = cart.TotalAmount.ToString("C");

            return Json(new
            {
                success = true,
                updatedTotalPrice = updatedTotalPrice,
                totalCartAmount = totalCartAmount
            });
        }
        [HttpGet]
        public IActionResult Checkout()
        {
            var cart = orderService.GetCartFromSession(HttpContext.Session);

            // If the cart is empty, redirect to the order index page
            if (cart == null || cart.Products == null || !cart.Products.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Order");
            }

            var model = new OrderViewModel
            {
                Products = cart.Products
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid. Reloading cart data...");

                // 🟢 Reload the cart from the session and update the model
                var cart = orderService.GetCartFromSession(HttpContext.Session);

                var updatedModel = new OrderViewModel
                {
                    Products = cart.Products, // 🟢 Populate Products for Order Summary
                    ShippingAddress = model.ShippingAddress,
                    ShippingCity = model.ShippingCity,
                    ShippingZip = model.ShippingZip
                };

                return View(updatedModel); // 🟢 Return the view with the updated model
            }

            var cartFromSession = orderService.GetCartFromSession(HttpContext.Session);
            if (cartFromSession == null || cartFromSession.Products == null || !cartFromSession.Products.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty.";
                return RedirectToAction("Index", "Order");
            }

            var customerId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var order = new Order
            {
                CustomerId = customerId,
                ShippingAddress = model.ShippingAddress,
                ShippingCity = model.ShippingCity,
                ShippingZip = model.ShippingZip,
                OrderStatus = "Pending",
                OrderDate = DateTime.UtcNow,
                OrderProducts = cartFromSession.Products.Select(p => new OrderProduct
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity
                }).ToList()
            };

            await orderService.PlaceOrderAsync(order);
            HttpContext.Session.Remove("ShoppingCart");

            TempData["SuccessMessage"] = "Your order has been placed successfully!";
            return RedirectToAction("History", "Order");
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var cartt = orderService.GetCartFromSession(HttpContext.Session);
                model.Products = cartt.Products; // Reload products so user can see their order
                return View("Checkout", model);
            }

            var customerId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            // 1️⃣ Load the cart from the session
            var cart = orderService.GetCartFromSession(HttpContext.Session);

            // 2️⃣ Check if the cart is empty
            if (cart == null || !cart.Products.Any())
            {
                TempData["ErrorMessage"] = "Your cart is empty. Please add some products.";
                return RedirectToAction("Index", "Store");
            }

            // 3️⃣ Create the Order object
            var order = new Order
            {
                CustomerId = customerId,
                ShippingAddress = model.ShippingAddress,
                ShippingCity = model.ShippingCity,
                ShippingZip = model.ShippingZip,
                OrderStatus = "Pending",
                OrderDate = DateTime.UtcNow,
                OrderProducts = cart.Products.Select(p => new OrderProduct
                {
                    ProductId = p.Id,
                    Quantity = p.Quantity
                }).ToList()
            };

            // 4️⃣ Check if OrderProducts is empty
            if (order.OrderProducts == null || !order.OrderProducts.Any())
            {
                TempData["ErrorMessage"] = "There are no products in the order.";
                return RedirectToAction("Index", "Store");
            }

            // 5️⃣ Place the order
            await orderService.PlaceOrderAsync(order);
            HttpContext.Session.Remove("ShoppingCart");

            TempData["SuccessMessage"] = "Your order has been placed successfully!";
            return RedirectToAction("History");
        }



        [HttpGet]
public async Task<IActionResult> History()
{
    var userId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

    if (string.IsNullOrEmpty(userId))
    {
        TempData["ErrorMessage"] = "User is not logged in.";
        return RedirectToAction("Index", "Store");
    }

    var orders = await orderService.GetUserOrdersAsync(userId);

    var orderHistory = orders.Select(o => new OrderHistoryViewModel
    {
        OrderId = o.Id,
        OrderDate = o.OrderDate,
        ShippingAddress = o.ShippingAddress,
        ShippingCity = o.ShippingCity,
        ShippingZip = o.ShippingZip,
        OrderStatus = o.OrderStatus,
        OrderTotal = o.OrderProducts.Sum(op => op.Product.Price * op.Quantity) // Calculate Total
    }).ToList();

    return View(orderHistory);
}


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = HttpContext.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["ErrorMessage"] = "User is not logged in.";
                return RedirectToAction("Index", "Store");
            }

            var order = await orderService.GetOrderDetailsAsync(id, userId);

            if (order == null)
            {
                TempData["ErrorMessage"] = "Order not found.";
                return RedirectToAction("History");
            }

            var orderDetails = new OrderDetailsViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                ShippingAddress = order.ShippingAddress,
                ShippingCity = order.ShippingCity,
                ShippingZip = order.ShippingZip,
                OrderStatus = order.OrderStatus,
                OrderTotal = order.OrderProducts.Sum(op => op.Product.Price * op.Quantity),
                Products = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    Name = op.Product.Name,
                    Quantity = op.Quantity,
                    Price = op.Product.Price
                }).ToList()
            };

            return View(orderDetails);
        }



        public class ProductQuantityUpdateRequest
        {
            public int ProductId { get; set; } 
            public int Quantity { get; set; }  
        }

    }

}

