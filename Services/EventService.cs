using Men_Of_Varna.Data;
using Men_Of_Varna.Contracts;
using Men_Of_Varna.Models.Events;
using Microsoft.EntityFrameworkCore;
using Men_Of_Varna.Data.Models;

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
                    IsAttending = e.UserEvents.Any(ue => ue.UserId == userId),
                   IsUpcoming = e.IsUpcoming,
                })
                .ToListAsync();
        }

        public async Task AddEventAsync(AddEventViewModel model, string userId)
        {
            // Ensure the published date is correctly parsed
            var publishedOnDate = model.PublishedOn; // In this case, no need to parse as it's a DateTime

            // Create the Event object
            var eventEntity = new Event
            {
                Name = model.Name,
                Description = model.Description,
                PictureUrl = model.ImageUrl, // Assuming ImageUrl is the field for the picture
                PublishedOn = publishedOnDate,
                CreatedBy = userId, // Assuming the userId is passed for tracking who created the event
                IsUpcoming = model.IsUpcoming
            };

            // Add the event to the database context
            dbContext.Events.Add(eventEntity);
            await dbContext.SaveChangesAsync(); // Save changes to the database
        }

        public async Task<Event> GetEventDetailsAsync(int eventId)
        {
            var eventDetails = await dbContext.Events
                .Include(e => e.Comments)  // Include the comments related to the event
                .FirstOrDefaultAsync(e => e.Id == eventId);

            return eventDetails;
        }

        // Add a comment to an event
        public async Task AddCommentAsync(int eventId, string content, string author)
        {
            var newComment = new Comment
            {
                Content = content,
                Author = author,
                CreatedAt = DateTime.Now,
                EventId = eventId
            };

            dbContext.Comments.Add(newComment);
            await dbContext.SaveChangesAsync();
        }


    }

}
