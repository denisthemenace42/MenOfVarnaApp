using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Feedback;

namespace Men_Of_Varna.Contracts
{
    public interface IFeedbackService
    {
        Task SubmitFeedbackAsync(Feedback feedback);

        Task DeleteFeedbackAsync(int id);
        Task<IEnumerable<FeedbackViewModel>> GetAllFeedbacksAsync();
    }
}
