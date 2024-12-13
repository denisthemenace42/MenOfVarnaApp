using Men_Of_Varna.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService orderService;
        private readonly UserManager<IdentityUser> userManager;

        public OrderController(IOrderService orderService, UserManager<IdentityUser> userManager)
        {
            this.orderService = orderService;
            this.userManager = userManager;

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
