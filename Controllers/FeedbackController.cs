using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Feedback;
using Men_Of_Varna.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> Index()
        {
            
            var viewModel = new FeedbackViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(FeedbackViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = userManager.GetUserId(User);
                var feedback = new Feedback
                {
                    Content = model.Content,
                    UserId = userId,  // Assuming you're using identity
                    SubmittedOn = DateTime.UtcNow
                };

                await feedbackService.SaveFeedbackAsync(feedback);  // Save feedback via service layer

                return RedirectToAction("ThankYou");  // Redirect to a thank-you page
            }

            return View(model);  // If invalid, return the form with errors
        }

        // Thank you page after successful feedback submission
        public IActionResult ThankYou()
        {
            return View();
        }
    }
}
