using MOV.Data.Models;
using MOV.ViewModels.Feedback;

namespace MOV.Services.Data.Interfaces
{
    public interface IFeedbackService
    {
        Task SubmitFeedbackAsync(Feedback feedback);

        Task DeleteFeedbackAsync(int id);
        Task<IEnumerable<FeedbackViewModel>> GetAllFeedbacksAsync();
    }
}
