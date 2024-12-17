using Xunit;
using Microsoft.EntityFrameworkCore;
using MOV.Services;
using MOV.Data;
using MOV.ViewModels.Events;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using MOV.Data;
using MOV.Data.Models;
using MOV.Services.Data;

namespace Tests.UnitTests.Services
{
    public class EventServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly EventService _eventService;

        public EventServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            SeedData(_dbContext);

            _eventService = new EventService(_dbContext);
        }

        private void SeedData(ApplicationDbContext dbContext)
        {
            var users = new List<IdentityUser>
            {
                new IdentityUser { Id = "user1", Email = "user1@example.com" },
                new IdentityUser { Id = "user2", Email = "user2@example.com" }
            };

            dbContext.Users.AddRange(users);

            var events = new List<Event>
            {
                new Event { Id = 111, Name = "Event 1", Description = "Description 1", PictureUrl = "img1.jpg", PublishedOn = DateTime.UtcNow, CreatedBy = "user1", IsUpcoming = true },
                new Event { Id = 222, Name = "Event 2", Description = "Description 2", PictureUrl = "img2.jpg", PublishedOn = DateTime.UtcNow, CreatedBy = "user2", IsUpcoming = true }
            };

            dbContext.Events.AddRange(events);

            var userEvents = new List<UserEvent>
            {
                new UserEvent { UserId = "user1", EventId = 1, JoinedOn = DateTime.UtcNow }
            };

            dbContext.UserEvents.AddRange(userEvents);

            dbContext.SaveChanges();
        }


        [Fact]
        public async Task AddEventAsync_ShouldAddNewEventToDatabase()
        {

            var newEvent = new AddEventViewModel
            {
                Name = "New Event",
                Description = "New Description",
                ImageUrl = "new.jpg",
                PublishedOn = DateTime.UtcNow,
                IsUpcoming = true
            };

            await _eventService.AddEventAsync(newEvent, "user2");

            var eventInDb = await _dbContext.Events.FirstOrDefaultAsync(e => e.Name == "New Event");
            Assert.NotNull(eventInDb);
            Assert.Equal("New Description", eventInDb.Description);
            Assert.Equal("new.jpg", eventInDb.PictureUrl);
        }

        [Fact]
        public async Task GetEventDetailsAsync_ShouldReturnEventDetails()
        {
            var result = await _eventService.GetEventDetailsAsync(111, "user1");

            Assert.NotNull(result);
            Assert.Equal("Event 1", result.Name);
            Assert.Equal("Description 1", result.Description);
        }

        [Fact]
        public async Task DeleteEventAsync_ShouldRemoveEventFromDatabase()
        {
            await _eventService.DeleteEventAsync(1);

            var eventInDb = await _dbContext.Events.FirstOrDefaultAsync(e => e.Id == 1);
            Assert.Null(eventInDb);
        }

        [Fact]
        public async Task AddToMyEventsAsync_ShouldAddUserToEvent()
        {
            var eventId = 2;
            var userId = "user1";

            await _eventService.AddToMyEventsAsync(eventId, userId);
            var userEvent = await _dbContext.UserEvents.FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == eventId);
            Assert.NotNull(userEvent);
        }

        [Fact]
        public async Task RemoveUserFromEventAsync_ShouldRemoveUserFromEvent()
        {
            var eventId = 1;
            var userId = "user1";
            var result = await _eventService.RemoveUserFromEventAsync(userId, eventId);

            Assert.True(result);
            var userEvent = await _dbContext.UserEvents.FirstOrDefaultAsync(ue => ue.UserId == userId && ue.EventId == eventId);
            Assert.Null(userEvent);
        }
    }
}
