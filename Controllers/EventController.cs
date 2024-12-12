using Men_Of_Varna.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

            return View(events);
        }
    }
}
