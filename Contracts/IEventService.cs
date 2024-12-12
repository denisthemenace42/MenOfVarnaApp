using Men_Of_Varna.Models.Events;

namespace Men_Of_Varna.Contracts
{
    public interface IEventService
    {
        Task<IEnumerable<EventViewModel>> GetAllEventsAsync(string userId);
    }
}
