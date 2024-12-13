using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Feedback;

namespace Men_Of_Varna.Contracts
{
    public interface IFeedbackService
    {
        Task<FeedbackViewModel> GetFeedbackDetailsAsync(int id);

        Task SaveFeedbackAsync(Feedback feedback);
    }
}
