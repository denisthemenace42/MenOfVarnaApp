using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MOV.Services.Data.Interfaces;
using System.Threading.Tasks;

namespace Men_Of_Varna.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class FeedbackManagementController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackManagementController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        public async Task<IActionResult> Index()
        {

            var feedbacks = await _feedbackService.GetAllFeedbacksAsync();

            return View(feedbacks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _feedbackService.DeleteFeedbackAsync(id);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error deleting feedback: {ex.Message}";
            }

            return RedirectToAction("Index");
        }
    }
}
