using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Events;
using Men_Of_Varna.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

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
            var userId = userManager.GetUserId(User); // Retrieve the logged-in user's ID
            var eventDetailViewModel = await eventService.GetEventDetailsAsync(id, userId);

            if (eventDetailViewModel == null)
            {
                return NotFound(); // Handle if event is not found
            }

            return View(eventDetailViewModel); // Return the view with the view model
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

        public async Task<IActionResult> Delete(int id)
        {
            var userId = userManager.GetUserId(User);
            var destination = await eventService.GetEventByIdAsync(id);

            if (destination == null)
            {
                return View();
            }

            var model = new DeleteEventViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                CreatedBy = destination.CreatedBy,
            
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteEventViewModel model)
        {
            var userId = userManager.GetUserId(User);
            var destination = await eventService.GetEventByIdAsync(model.Id);

            if (destination == null)
            {
                return View(model);

            }

            await eventService.DeleteEventAsync(model.Id);


            return RedirectToAction("Index", "Event");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var userId = userManager.GetUserId(User);
            var destination = await eventService.GetEventByIdAsync(id);

            if (destination == null)
            {
                return View();
            }

            var model = new EditEventViewModel
            {
                Id = destination.Id,
                Name = destination.Name,
                Description = destination.Description,
                PictureUrl = destination.PictureUrl,
                PublishedOn = destination.PublishedOn,
                CreatedBy = destination.CreatedBy,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditEventViewModel model)
        {
            var userId = userManager.GetUserId(User);
            var destination = await eventService.GetEventByIdAsync(model.Id);

            if (destination == null)
            {
                return View();
            }

            destination.Name = model.Name;
            destination.Description = model.Description;
            destination.PictureUrl = model.PictureUrl;
            destination.PublishedOn = model.PublishedOn;


            await eventService.UpdateEventAsync(destination);

            return RedirectToAction("Details", new { id = model.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Attend(int id)
        {
            var userId = userManager.GetUserId(User);
            await eventService.AddToMyEventsAsync(id, userId);

            var eventModel = await eventService.GetEventDetailsAsync(id,userId);
            eventModel.AttendeesCount = await eventService.GetAttendeesCountAsync(id);

            eventModel.IsAttending = true;

            return RedirectToAction("Details", new { id = id });

        }

        public async Task<IActionResult> MyEvents()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var myEvents = await eventService.GetMyEventsAsync(userId);

            var viewModel = new MyEventsViewModel
            {
                Events = myEvents.ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> GiveUpOnEvent(int eventId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return Unauthorized();
            }

            var result = await eventService.RemoveUserFromEventAsync(userId, eventId);

            if (!result)
            {
                return NotFound(); // Return not found if something goes wrong
            }

            // Redirect back to the My Events page
            return RedirectToAction("MyEvents");
        }

        


    }
}
