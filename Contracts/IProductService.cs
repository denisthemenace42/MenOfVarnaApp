using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Store;

namespace Men_Of_Varna.Contracts
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
