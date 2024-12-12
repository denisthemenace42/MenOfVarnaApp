using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Events;
using Men_Of_Varna.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Men_Of_Varna.Controllers
{
    public class EventController : BaseController
    {

        private readonly IEventService eventService;
        private readonly UserManager<IdentityUser> userManager;

        public EventController(IEventService eventService, UserManager<IdentityUser> userManager)
        {
            this.eventService = eventService;
            this.userManager = userManager;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var userId = userManager.GetUserId(User);
            var events = await eventService.GetAllEventsAsync(userId);
            var orderedEvents = events.OrderByDescending(e => e.PublishedOn).ToList();

            return View(orderedEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new AddEventViewModel
            {
                PublishedOn = DateTime.Now // Default value for PublishedOn
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.Name; // Assuming the user is authenticated
                await eventService.AddEventAsync(model, userId);
                return RedirectToAction("Index"); // Redirect to event list or another page
            }

            return View(model); // Return the model with validation errors if not valid
        }

        public async Task<IActionResult> Details(int id)
        {
            var eventDetails = await eventService.GetEventDetailsAsync(id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        // Add Comment Action - Adds a comment to the event
        [HttpPost]
        public async Task<IActionResult> AddComment(int eventId, string content)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                var author = User.Identity.Name;
                await eventService.AddCommentAsync(eventId, content, author);

                return RedirectToAction("Details", new { id = eventId });
            }

            return RedirectToAction("Login", "Account");  // Redirect to login if the user is not authenticated
        }


    }
}
