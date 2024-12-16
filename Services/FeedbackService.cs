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

        public async Task<IEnumerable<FeedbackViewModel>> GetAllFeedbacksAsync()
        {
            var feedbacks = await dbContext.Feedbacks
                .Join(dbContext.Users,
                      feedback => feedback.UserId,
                      user => user.Id,
                      (feedback, user) => new FeedbackViewModel
                      {
                          Id = feedback.Id,
                          UserId = user.Email, 
                          Content = feedback.Content,
                          SubmittedOn = feedback.SubmittedOn
                      })
                .ToListAsync();

            return feedbacks;
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await dbContext.Feedbacks.FindAsync(id);

            if (feedback != null)
            {
                dbContext.Feedbacks.Remove(feedback);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Feedback not found.");
            }
        }

    }
}
