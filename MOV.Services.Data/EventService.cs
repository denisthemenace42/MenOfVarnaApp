using MOV.Data;
using Microsoft.EntityFrameworkCore;
using MOV.Data.Models;
using MOV.Services.Data.Interfaces;
using MOV.ViewModels.Events;

namespace MOV.Services.Data
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
                    AttendeesCount = e.UserEvents.Count,
                    IsPublisher = e.CreatedBy == userId,
                    IsAttending = e.UserEvents.Any(ue => ue.UserId == userId),
                    IsUpcoming = e.IsUpcoming,
                })
                .ToListAsync();
        }

        public async Task AddEventAsync(AddEventViewModel model, string userId)
        {
            var publishedOnDate = model.PublishedOn;
            var eventEntity = new Event
            {
                Name = model.Name,
                Description = model.Description,
                PictureUrl = model.ImageUrl,
                PublishedOn = publishedOnDate,
                CreatedBy = userId,
                IsUpcoming = model.IsUpcoming
            };

            dbContext.Events.Add(eventEntity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<EventDetailViewModel> GetEventDetailsAsync(int eventId, string userId)
        {
            var eventDetails = await dbContext.Events
                .Include(e => e.Comments)
                .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventDetails == null)
            {
                return null;
            }


            var isAttending = await dbContext.UserEvents
                .AnyAsync(ue => ue.UserId == userId && ue.EventId == eventId);

            var attendeesCount = await dbContext.UserEvents
                .CountAsync(ue => ue.EventId == eventId);

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
            var existingEntry = await dbContext.UserEvents
                .FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == eventId);

            if (existingEntry != null)
            {
                throw new InvalidOperationException("The user is already attending this event.");
            }

            var userEvent = new UserEvent
            {
                UserId = userId,
                EventId = eventId,
                JoinedOn = DateTime.UtcNow
            };

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
                return false;
            }

            dbContext.UserEvents.Remove(userEvent);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetAttendeesCountAsync(int eventId)
        {
            return await dbContext.UserEvents.CountAsync(ue => ue.EventId == eventId);
        }
    }
}
