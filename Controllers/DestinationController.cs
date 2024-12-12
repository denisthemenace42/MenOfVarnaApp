using Horizons.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Horizons.Contracts;
using Horizons.Controllers;
using Horizons.ViewModels;
using Microsoft.EntityFrameworkCore;
using Horizons.Data;
using Horizons.Models;
using Microsoft.AspNetCore.Authorization;
using Horizons.Data.Models;

public class DestinationController : BaseController
{
  
    private readonly IDestinationService destinationService;
    private readonly UserManager<IdentityUser> userManager;



    public DestinationController(IDestinationService destinationService, UserManager<IdentityUser> userManager)
    {
        this.destinationService = destinationService;
        this.userManager = userManager;

    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var userId = userManager.GetUserId(User);
        var destinations = await destinationService.GetAllDestinationsAsync(userId);

        return View(destinations);
    }

    [HttpGet]
    public async Task<IActionResult> Add()
    {
        var model = new AddDestinationViewModel
        {
            Terrains = await destinationService.GetAllTerrainsAsync()
        };

        return View(model);
    }

  
    [HttpPost]
    public async Task<IActionResult> Add(AddDestinationViewModel model)
    {
        Console.WriteLine($"TerrainId: {model.TerrainId}");

        if (!ModelState.IsValid)
        {
   
            model.Terrains = await destinationService.GetAllTerrainsAsync();

            return View(model);
        }

        var userId = userManager.GetUserId(User);
        await destinationService.AddDestinationAsync(model, userId);

        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Favorites()
    {
        var userId = userManager.GetUserId(User); 
        var model = await destinationService.GetUserFavoritesAsync(userId);


        return View(model);
    }




    [AllowAnonymous]
    public async Task<IActionResult> Details(int id)
    {
        var userId = userManager.GetUserId(User); 

        var destination = await destinationService.GetDestinationByIdAsync(id);

        if (destination == null)
        {
            return View();
        }

        bool isPublisher = destination.PublisherId == userId;

        bool isFavorite = await destinationService.IsFavoriteAsync(id, userId);

        var model = new DestinationDetailsViewModel
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            ImageUrl = destination.ImageUrl,
            Terrain = destination.Terrain.Name, 
            PublishedOn = destination.PublishedOn,
            Publisher = destination.Publisher.UserName,
            IsPublisher = isPublisher,
            IsFavorite = isFavorite
        };

        return View(model);
    }


    public async Task<IActionResult> Delete(int id)
    {
        var userId = userManager.GetUserId(User); 
        var destination = await destinationService.GetDestinationByIdAsync(id);

        if (destination == null)
        {
            return View();
        }

       
        if (destination.PublisherId != userId)
        {
            return RedirectToAction("Index");
        }

        var model = new DeleteDestinationViewModel
        {
            Id = destination.Id,
            Name = destination.Name,
            Publisher = destination.Publisher.UserName, 
            PublisherId = destination.PublisherId
        };

        return View(model);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(DeleteDestinationViewModel model)
    {
        var userId = userManager.GetUserId(User);
        var destination = await destinationService.GetDestinationByIdAsync(model.Id);

        if (destination == null)
        {
            return View();
        }

        
        if (destination.PublisherId != userId)
        {
            return RedirectToAction("Index");
        }

        
        await destinationService.SoftDeleteDestinationAsync(model.Id);

       
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Edit(int id)
    {
        var userId = userManager.GetUserId(User);
        var destination = await destinationService.GetDestinationByIdAsync(id);
        var terrains = await destinationService.GetAllTerrainsAsync();

        if (destination == null)
        {
            return View();
        }

        if (destination.PublisherId != userId)
        {
            return RedirectToAction("Index");
        }

       
        var model = new EditDestinationViewModel
        {
            Id = destination.Id,
            Name = destination.Name,
            Description = destination.Description,
            ImageUrl = destination.ImageUrl,
            PublishedOn = destination.PublishedOn,
            TerrainId = destination.TerrainId, 
            PublisherId = destination.PublisherId, 
            Terrains = terrains 
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(EditDestinationViewModel model)
    {
        

        var userId = userManager.GetUserId(User); 
        var destination = await destinationService.GetDestinationByIdAsync(model.Id);

        if (destination == null)
        {
            return View();
        }

      
        if (destination.PublisherId != userId)
        {
            return RedirectToAction("Index");
        }

    
        destination.Name = model.Name;
        destination.Description = model.Description;
        destination.ImageUrl = model.ImageUrl;
        destination.PublishedOn = model.PublishedOn;
        destination.TerrainId = model.TerrainId;

      
        await destinationService.UpdateDestinationAsync(destination);

      
        return RedirectToAction("Details", new { id = model.Id });
    }

    [HttpPost]
    public async Task<IActionResult> AddToFavorites(int id, string returnUrl)
    {
        var userId = userManager.GetUserId(User); 

        var isFavorite = await destinationService.IsFavoriteAsync(id, userId);
        if (isFavorite)
        {
            return Redirect(returnUrl ?? "/Destination/Index"); 
        }

        await destinationService.AddToFavoritesAsync(id, userId);

        return Redirect(returnUrl ?? "/Destination/Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromFavorites(int id)
    {
        var userId = userManager.GetUserId(User);

        await destinationService.RemoveFromFavoritesAsync(id, userId);

        
        return RedirectToAction("Favorites");
    }
}


