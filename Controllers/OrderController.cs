using Men_Of_Varna.Contracts;
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
        public async Task<IActionResult> Checkout()
        {
            var userId = userManager.GetUserId(User);
            await orderService.CheckoutAsync(HttpContext.Session, userId);
            TempData["SuccessMessage"] = "Order placed successfully!";
            return RedirectToAction("Index", "Store");
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
        public class ProductQuantityUpdateRequest
        {
            public int ProductId { get; set; } 
            public int Quantity { get; set; }  
        }

    }

}

