using Men_Of_Varna.Data;
using Men_Of_Varna.Contracts;
using Men_Of_Varna.Models.Events;
using Microsoft.EntityFrameworkCore;

namespace Men_Of_Varna.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext dbContext;

        public EventService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<EventViewModel>> GetAllEventsAsync(string userId)
        {
            return await dbContext
                .Events
                .Select(e => new EventViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    ImageUrl = e.PictureUrl,
                    Description = e.Description,
                    PublishedOn = e.PublishedOn,
                    AttendeesCount = e.UserEvents.Count, // Assuming a many-to-many relation between Event and User
                    IsPublisher = e.CreatedBy == userId,
                    IsAttending = e.UserEvents.Any(ue => ue.UserId == userId) // Check if the user is attending this event
                })
                .ToListAsync();
        }

    }

}
