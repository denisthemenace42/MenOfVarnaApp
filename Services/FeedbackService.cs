using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Feedback;
using Microsoft.EntityFrameworkCore;

namespace Men_Of_Varna.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext dbContext;

        public FeedbackService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task SubmitFeedbackAsync(Feedback feedback)
        {
            dbContext.Feedbacks.Add(feedback);
            await dbContext.SaveChangesAsync();
        }

    }
}
