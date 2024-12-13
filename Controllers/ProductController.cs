using Men_Of_Varna.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductService productService;
        private readonly UserManager<IdentityUser> userManager;

        public ProductController(IProductService productService, UserManager<IdentityUser> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
