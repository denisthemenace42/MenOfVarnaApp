using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data;

namespace Men_Of_Varna.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;

        public ProductService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
