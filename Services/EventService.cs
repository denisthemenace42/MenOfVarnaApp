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

        public async Task<EventDetailViewModel> GetEventDetailsAsync(int eventId, string userId)
        {
            var eventDetails = await dbContext.Events
                .Include(e => e.Comments)  // Include comments related to the event
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetails == null)
            {
                return null; // Handle event not found
            }

            // Check if the user is attending the event
            var isAttending = await dbContext.UserEvents
                .AnyAsync(ue => ue.UserId == userId && ue.EventId == eventId);

            // Get the number of attendees
            var attendeesCount = await dbContext.UserEvents
                .CountAsync(ue => ue.EventId == eventId);

            // Create the view model
            var eventDetailViewModel = new EventDetailViewModel
            {
                Id = eventDetails.Id,
                Name = eventDetails.Name,
                Description = eventDetails.Description,
                PictureUrl = eventDetails.PictureUrl,
                PublishedOn = eventDetails.PublishedOn,
                CreatedBy = eventDetails.CreatedBy,
                IsUpcoming = eventDetails.IsUpcoming,
                AttendeesCount = attendeesCount,
                IsAttending = isAttending,
                Comments = eventDetails.Comments.Select(c => new CommentViewModel
                {
                    Author = c.Author,
                    CreatedAt = c.CreatedAt,
                    Content = c.Content
                }).ToList(),
            };

            return eventDetailViewModel;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await dbContext.Events
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task DeleteEventAsync(int eventId)
        {
            var eventt = await dbContext.Events.FirstOrDefaultAsync(d => d.Id == eventId);

            if (eventt != null)
            {
                dbContext.Events.Remove(eventt);
                await dbContext.SaveChangesAsync();
            }
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

        public async Task UpdateEventAsync(Event destination)
        {
            var existingDestination = await dbContext.Events
                .FirstOrDefaultAsync(d => d.Id == destination.Id);

            if (existingDestination == null)
            {
                throw new Exception("Destination not found");
            }

            existingDestination.Name = destination.Name;
            existingDestination.Description = destination.Description;
            existingDestination.PictureUrl = destination.PictureUrl;
            existingDestination.PublishedOn = destination.PublishedOn;

            await dbContext.SaveChangesAsync();
        }

        public async Task<bool> IsAttendAsync(int destinationId, string userId)
        {
            return await dbContext.UserEvents
                .AnyAsync(ud => ud.UserId == userId && ud.EventId == destinationId);
        }

        public async Task AddToMyEventsAsync(int eventId, string userId)
        {
            // Check if the user is already attending the event
            var existingEntry = await dbContext.UserEvents
                .FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == eventId);

            if (existingEntry != null)
            {
                throw new InvalidOperationException("The user is already attending this event.");
            }

            // Create a new UserEvent instance
            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = eventId,
                JoinedOn = DateTime.UtcNow
            };

            // Add to the DbSet and save changes
            await dbContext.UserEvents.AddAsync(userEvent);
            await dbContext.SaveChangesAsync();
        }

        public async Task<List<MyEventViewModel>> GetMyEventsAsync(string userId)
        {
            return await dbContext.UserEvents
                .Where(ue => ue.UserId == userId)
                .Select(ue => new MyEventViewModel
                {
                    Id = ue.EventId,
                    Name = ue.Event.Name,
                    PictureUrl = ue.Event.PictureUrl ?? string.Empty,
                    Description = ue.Event.Description ?? string.Empty
                })
                .ToListAsync();
        }

        public async Task<bool> RemoveUserFromEventAsync(string userId, int eventId)
        {
            var userEvent = await dbContext.UserEvents
                .FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == eventId);

            if (userEvent == null)
            {
                return false; // If the user is not associated with the event, return false
            }

            dbContext.UserEvents.Remove(userEvent);
            await dbContext.SaveChangesAsync();

            return true; // Successfully removed the user from the event
        }

        public async Task<int> GetAttendeesCountAsync(int eventId)
        {
            return await dbContext.UserEvents.CountAsync(ue => ue.EventId == eventId);
        }

        



    }

}
