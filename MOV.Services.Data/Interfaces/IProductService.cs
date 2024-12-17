using MOV.Data.Models;
using MOV.ViewModels.Store;

namespace MOV.Services.Data.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllActiveProductsAsync();

        Task AddProductAsync(AddProductViewModel product);

        Task<Product?> GetByIdAsync(int id);

        Task AddCommentAsync(int productId, string author, string content);

        Task DeleteProductAsync(int id);

        Task UpdateProductAsync(Product product);
    }
}
