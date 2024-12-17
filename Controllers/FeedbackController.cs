using MOV.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MOV.Services.Data.Interfaces;
using MOV.ViewModels.Feedback;

namespace Men_Of_Varna.Controllers
{
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService feedbackService;
        private readonly UserManager<IdentityUser> userManager;


        public FeedbackController(IFeedbackService feedbackService, UserManager<IdentityUser> userManager)
        {
            this.feedbackService = feedbackService;
            this.userManager = userManager;

        }

        [HttpGet]
        public IActionResult Index(int? eventId, int? productId)
        {
            var model = new FeedbackViewModel { };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Submit(FeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var userId = userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized();
            }

            var feedback = new Feedback
            {
                UserId = userId,
                Content = model.Content,
                SubmittedOn = DateTime.UtcNow,

            };

            await feedbackService.SubmitFeedbackAsync(feedback);

            TempData["SuccessMessage"] = "Your feedback has been submitted successfully.";
            return RedirectToAction("Index", "Feedback");
        }

    }
}
