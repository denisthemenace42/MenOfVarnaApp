using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Men_Of_Varna.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllActiveProductsAsync()
        {
            return await dbContext.Products
                .Where(p => p.IsActive)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    PictureUrl = p.PictureUrl,
                    StockQuantity = p.StockQuantity,
                    Category = p.Category,
                    IsActive = p.IsActive
                })
                .ToListAsync();
        }

        public async Task AddProductAsync(AddProductViewModel product)
        {
            var model = new Product
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                PictureUrl = product.PictureUrl,
                StockQuantity = product.StockQuantity,
                Category = product.Category,
                IsActive = product.IsActive
            };
            dbContext.Products.Add(model);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await dbContext.Products
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddCommentAsync(int productId, string author, string content)
        {
            var product = await GetByIdAsync(productId);
            if (product == null) throw new Exception("Product not found");

            product.Comments.Add(new Comment
            {
                Author = author,
                CreatedAt = DateTime.UtcNow,
                Content = content,
                ProductId = productId
            });

            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                dbContext.Remove(product);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(Product product)
        {
            dbContext.Update(product);
            await dbContext.SaveChangesAsync();
        }
    }
}
