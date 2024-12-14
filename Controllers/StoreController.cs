using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data.Models;
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
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                

                await productService.AddProductAsync(model);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new ProductDetailViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive,
                Comments = product.Comments.Select(c => new CommentViewModel
                {
                    Author = c.Author,
                    CreatedAt = c.CreatedAt,
                    Content = c.Content
                }).ToList()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int productId, string content)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }

            await productService.AddCommentAsync(productId, User.Identity.Name, content);
            return RedirectToAction("Details", new { id = productId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new DeleteProductViewModel
            {
                Id = product.Id,
                Name = product.Name
            };

            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteProductViewModel model)
        {
            await productService.DeleteProductAsync(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var model = new EditProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive
            };

            return View(model);
        }

        // POST: Store/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
                PictureUrl = model.PictureUrl,
                StockQuantity = model.StockQuantity,
                Category = model.Category,
                IsActive = model.IsActive
            };

            await productService.UpdateProductAsync(product);

            return RedirectToAction("Details", new { id = product.Id });
        }


    }
}
