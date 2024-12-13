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


        public async Task<FeedbackViewModel> GetFeedbackDetailsAsync(int id)
        {
            var feedback = await dbContext.Feedbacks
         .Where(f => f.Id == id)
         .FirstOrDefaultAsync();

            if (feedback == null)
            {
                return null;  // Handle not found case if needed
            }

            return new FeedbackViewModel
            {
              
                Content = feedback.Content,  // Populate the content field with existing feedback
                                             // Add other necessary properties here
            };
        }

        public async Task SaveFeedbackAsync(Feedback feedback)
        {
            dbContext.Feedbacks.Add(feedback);
            await dbContext.SaveChangesAsync();
        }

    }
}
