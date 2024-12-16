using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HandleErrorCode(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("404");
            }
            else if (statusCode == 500)
            {
                return View("500");
            }
            else
            {
                return View("GenericError");
            }
        }

        [Route("Error/500")]
        public IActionResult HandleServerError()
        {
            return View("500");
        }
    }
}
