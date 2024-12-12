using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Horizons.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
       
    }
}
