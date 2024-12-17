using Xunit;
using Microsoft.EntityFrameworkCore;
using MOV.Services;
using MOV.Data;
using MOV.Data.Models;
using MOV.ViewModels.Store;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MOV.Services.Data;

namespace Tests.UnitTests.Services
{
    public class ProductServiceTests : IDisposable
    {
        private ApplicationDbContext _dbContext;
        private ProductService _productService;

        public ProductServiceTests()
        {
            SetupDbContext();
        }

        private void SetupDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();
            SeedData(_dbContext);
            _productService = new ProductService(_dbContext);
        }

        private void SeedData(ApplicationDbContext dbContext)
        {
            var products = new List<Product>
        {
            new Product { Id = 77, Name = "Product 1", Description = "Description 1", Price = 100, PictureUrl = "https://example.com/img1.jpg", StockQuantity = 10, Category = "Category 1", IsActive = true },
            new Product { Id = 321, Name = "Product 2", Description = "Description 2", Price = 200, PictureUrl = "https://example.com/img2.jpg", StockQuantity = 5, Category = "Category 2", IsActive = true },
            new Product { Id = 56, Name = "Product 3", Description = "Description 3", Price = 300, PictureUrl = "https://example.com/img3.jpg", StockQuantity = 0, Category = "Category 3", IsActive = false }
        };

            dbContext.Products.AddRange(products);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        [Fact]
        public async Task GetAllActiveProductsAsync_ShouldReturnOnlyActiveProducts()
        {
            var result = await _productService.GetAllActiveProductsAsync();
            Assert.NotNull(result);
            Assert.Equal(13, result.Count());
            Assert.DoesNotContain(result, p => p.Name == "Product 3");
        }

        [Fact]
        public async Task AddProductAsync_ShouldAddProductToDatabase()
        {
            var product = new AddProductViewModel
            {
                Name = "New Product",
                Description = "New Description",
                Price = 150,
                PictureUrl = "https://example.com/new.jpg",
                StockQuantity = 20,
                Category = "New Category",
                IsActive = true
            };

            await _productService.AddProductAsync(product);
            var productInDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.Name == "New Product");
            Assert.NotNull(productInDb);
            Assert.Equal("New Description", productInDb.Description);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnProductIfExists()
        {
            var result = await _productService.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal("Motivational T-Shirt", result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNullIfProductDoesNotExist()
        {
            var result = await _productService.GetByIdAsync(999);
            Assert.Null(result);
        }


        [Fact]
        public async Task AddCommentAsync_ShouldThrowExceptionIfProductNotFound()
        {
            await Assert.ThrowsAsync<Exception>(() => _productService.AddCommentAsync(999, "Author 1", "This is a comment."));
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldRemoveProductFromDatabase()
        {
            await _productService.DeleteProductAsync(1);
            var productInDb = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == 1);
            Assert.Null(productInDb);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDoNothingIfProductDoesNotExist()
        {
            await _productService.DeleteProductAsync(999);
            var productCount = await _dbContext.Products.CountAsync();
            Assert.Equal(14, productCount);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProductDetails()
        {
            var product = await _productService.GetByIdAsync(1);
            product.Name = "Updated Product";
            product.Description = "Updated Description";

            await _productService.UpdateProductAsync(product);
            var updatedProduct = await _productService.GetByIdAsync(1);

            Assert.NotNull(updatedProduct);
            Assert.Equal("Updated Product", updatedProduct.Name);
            Assert.Equal("Updated Description", updatedProduct.Description);
        }
    }
}
