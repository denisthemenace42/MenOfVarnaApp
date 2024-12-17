using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Events;
using Men_Of_Varna.Models.Store;
using Men_Of_Varna.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using CommentViewModel = Men_Of_Varna.Models.Store.CommentViewModel;

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
        public async Task<IActionResult> Index([FromQuery][MaxLength(100)] string searchQuery = "", [FromQuery][MaxLength(50)] string category = "", [FromQuery] decimal minPrice = 0, [FromQuery] decimal maxPrice = 0, int pageNumber = 1, int pageSize = 8)
        {
            var products = await productService.GetAllActiveProductsAsync();

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                             || p.Description.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrWhiteSpace(category))
            {
                products = products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (minPrice > 0)
            {
                products = products.Where(p => p.Price >= minPrice).ToList();
            }

            if (maxPrice > 0)
            {
                products = products.Where(p => p.Price <= maxPrice).ToList();
            }

            var totalProducts = products.Count();
            var paginatedProducts = products
                .OrderByDescending(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var viewModel = new StoreViewModel
            {
                Products = paginatedProducts,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalProducts / (double)pageSize),
                ItemsPerPage = pageSize
            };

            return View(viewModel);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromForm] AddProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await productService.AddProductAsync(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            var product = await productService.GetByIdAsync(id);
            if (product == null) return NotFound();

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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddComment([FromForm] int productId, [FromForm][Required][MaxLength(500)] string content)
        {
            if (!User.Identity.IsAuthenticated) return Unauthorized();

            if (productId <= 0 || string.IsNullOrWhiteSpace(content)) return BadRequest();

            await productService.AddCommentAsync(productId, User.Identity.Name, content);
            return RedirectToAction("Details", new { id = productId });
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            var product = await productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            var model = new DeleteProductViewModel
            {
                Id = product.Id,
                Name = product.Name
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromForm] DeleteProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            await productService.DeleteProductAsync(model.Id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id)
        {
            if (id <= 0) return BadRequest();

            var product = await productService.GetByIdAsync(id);
            if (product == null) return NotFound();

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromForm] EditProductViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

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
