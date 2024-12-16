using Men_Of_Varna.Areas.Admin.ViewModels;
using Men_Of_Varna.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderManagementController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderManagementController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: Admin/OrderManagement/Index
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            var viewModel = orders.Select(o => new OrderViewModel
            {
                Id = o.Id,
                CustomerEmail = o.Customer.Email,
                OrderDate = o.OrderDate,
                OrderStatus = o.OrderStatus,
                TotalAmount = o.OrderProducts.Sum(op => op.Quantity * op.Product.Price)
            }).ToList();

            return View(viewModel);
        }

        // POST: Admin/OrderManagement/ChangeOrderStatus
        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, string status)
        {
            await _orderService.UpdateOrderStatusAsync(orderId, status);
            TempData["SuccessMessage"] = "Order status updated successfully.";
            return RedirectToAction("Index");
        }

        // GET: Admin/OrderManagement/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            var viewModel = new OrderDetailViewModel
            {
                Id = order.Id,
                CustomerEmail = order.Customer.Email,
                OrderDate = order.OrderDate,
                OrderStatus = order.OrderStatus,
                ShippingAddress = order.ShippingAddress,
                ShippingCity = order.ShippingCity,
                ShippingZip = order.ShippingZip,
                Products = order.OrderProducts.Select(op => new OrderProductViewModel
                {
                    Id = op.Product.Id,
                    Name = op.Product.Name,
                    PictureUrl = op.Product.PictureUrl, // New: Picture for product
                    Price = op.Product.Price,
                    Quantity = op.Quantity
                }).ToList()
            };

            return View(viewModel);
        }

        // POST: Admin/OrderManagement/DeleteOrder
        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            await _orderService.DeleteOrderAsync(orderId);
            TempData["SuccessMessage"] = "Order deleted successfully.";
            return RedirectToAction("Index");
        }
    }
}
