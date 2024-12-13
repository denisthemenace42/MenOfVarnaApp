using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data;
namespace Men_Of_Varna.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext dbContext;

        public OrderService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
