using Men_Of_Varna.Contracts;
using Men_Of_Varna.Models.Store;
using Men_Of_Varna.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Controllers
{
    public class StoreController : BaseController
    {
        private readonly IProductService productService;
        private readonly UserManager<IdentityUser> userManager;

        public StoreController(IProductService productService, UserManager<IdentityUser> userManager)
        {
            this.productService = productService;
            this.userManager = userManager;

        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
{
    var products = await productService.GetAllActiveProductsAsync();
    var orderedProducts = products.OrderByDescending(p => p.Name).ToList();

    var viewModel = new StoreViewModel
    {
        Products = orderedProducts
    };

    return View(viewModel);
}

    }
}
