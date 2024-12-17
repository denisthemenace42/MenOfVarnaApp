using Xunit;
using Microsoft.EntityFrameworkCore;
using MOV.Services;
using MOV.Data;
using MOV.Data.Models;
using MOV.ViewModels.Feedback;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using MOV.Data;
using MOV.Services.Data;
using MOV.Data.Models;

namespace Tests.UnitTests.Services
{
    public class FeedbackServiceTests
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly FeedbackService _feedbackService;

        public FeedbackServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Database.EnsureCreated();

            SeedData(_dbContext);

            _feedbackService = new FeedbackService(_dbContext);
        }

        private void SeedData(ApplicationDbContext dbContext)
        {
            var users = new List<IdentityUser>
            {
                new IdentityUser { Id = "user1", Email = "user1@example.com", UserName = "user1@example.com" },
                new IdentityUser { Id = "user2", Email = "user2@example.com", UserName = "user2@example.com" }
            };

            dbContext.Users.AddRange(users);

            var feedbacks = new List<Feedback>
            {
                new Feedback { Id = 1, Content = "Feedback 1", SubmittedOn = DateTime.UtcNow, UserId = "user1" },
                new Feedback { Id = 2, Content = "Feedback 2", SubmittedOn = DateTime.UtcNow, UserId = "user2" }
            };

            dbContext.Feedbacks.AddRange(feedbacks);

            dbContext.SaveChanges();
        }

        [Fact]
        public async Task SubmitFeedbackAsync_ShouldAddFeedbackToDatabase()
        {
            var feedback = new Feedback
            {
                Id = 3,
                Content = "This is test feedback",
                SubmittedOn = DateTime.UtcNow,
                UserId = "user1"
            };

            await _feedbackService.SubmitFeedbackAsync(feedback);

            var feedbackInDb = await _dbContext.Feedbacks.FindAsync(feedback.Id);
            Assert.NotNull(feedbackInDb);
            Assert.Equal("This is test feedback", feedbackInDb.Content);
        }

        [Fact]
        public async Task GetAllFeedbacksAsync_ShouldReturnAllFeedbackViewModels()
        {
            var result = await _feedbackService.GetAllFeedbacksAsync();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Contains(result, f => f.Content == "Feedback 1");
            Assert.Contains(result, f => f.Content == "Feedback 2");
        }

        [Fact]
        public async Task DeleteFeedbackAsync_ShouldRemoveFeedbackFromDatabase()
        {
            var feedbackToDelete = await _dbContext.Feedbacks.FirstAsync();

            await _feedbackService.DeleteFeedbackAsync(feedbackToDelete.Id);

            var feedbackInDb = await _dbContext.Feedbacks.FindAsync(feedbackToDelete.Id);
            Assert.Null(feedbackInDb);
        }

        [Fact]
        public async Task DeleteFeedbackAsync_ShouldThrowExceptionIfFeedbackNotFound()
        {
            var exception = await Assert.ThrowsAsync<Exception>(() => _feedbackService.DeleteFeedbackAsync(999));
            Assert.Equal("Feedback not found.", exception.Message);
        }
    }
}
