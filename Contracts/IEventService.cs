using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Events;

namespace Men_Of_Varna.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> GetAllEventsAsync(string userId);

        Task AddEventAsync(AddEventViewModel model, string userId);

        Task<Event> GetEventDetailsAsync(int eventId);

        Task AddCommentAsync(int eventId, string content, string author);

    }
}
