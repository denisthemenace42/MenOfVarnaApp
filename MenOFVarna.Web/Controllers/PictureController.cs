using MenOFVarna.Data.Models;
using MenOFVarna.Web.Data;
using MenOfVarnaApp.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace MenOFVarna.Web.Controllers
{
    public class PictureController : Controller
    {
        private MenOfVarnaDbContext dbContext;

        public PictureController(MenOfVarnaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Picture> allPictures = this.dbContext
                .Pictures
                .ToList();

            return View(allPictures);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(Picture picture)
        {
            this.dbContext.Pictures.Add(picture);
            this.dbContext.SaveChanges();
            return this.RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Details(string id) 
        { 
            bool isIdValid = Guid.TryParse(id, out Guid guidId);
            if (!isIdValid)
            {
                return this.RedirectToAction(nameof(Index));
            }

            Picture? picture = this.dbContext.Pictures.FirstOrDefault( p => p.Id == guidId);
            if (picture == null) 
            { 
                return this.RedirectToAction(nameof(Index));
            }

            return this.View(picture);
        }
    }
}
