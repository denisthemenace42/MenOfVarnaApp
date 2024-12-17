using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}
