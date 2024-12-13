using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Events;

namespace Men_Of_Varna.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> GetAllEventsAsync(string userId);

        Task AddEventAsync(AddEventViewModel model, string userId);

        Task<EventDetailViewModel> GetEventDetailsAsync(int eventId, string userId);

        Task AddCommentAsync(int eventId, string content, string author);

        Task<Event> GetEventByIdAsync(int id);

        Task DeleteEventAsync(int eventId);

        Task UpdateEventAsync(Event destination);

        Task<bool> IsAttendAsync(int destinationId, string userId);

        Task AddToMyEventsAsync(int eventId, string userId);

        Task<List<MyEventViewModel>> GetMyEventsAsync(string userId);

        Task<bool> RemoveUserFromEventAsync(string userId, int eventId);

        Task<int> GetAttendeesCountAsync(int eventId);
    }
}
