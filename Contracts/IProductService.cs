using Men_Of_Varna.Models.Store;

namespace Men_Of_Varna.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllActiveProductsAsync();
    }
}
