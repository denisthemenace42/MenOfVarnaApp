using MOV.Data.Models;
using MOV.ViewModels.Events;

namespace MOV.Services.Data.Interfaces
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
