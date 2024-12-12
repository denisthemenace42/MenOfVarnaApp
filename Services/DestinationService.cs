using Men_Of_Varna.Contracts;
using Men_Of_Varna.Data;
using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Destinations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Men_Of_Varna.Services
{
    public class DestinationService : IDestinationService
    {
        private readonly ApplicationDbContext dbContext;

        public DestinationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<DestinationViewModel>> GetAllDestinationsAsync(string userId)
        {
            return await dbContext
                .Destinations
                .Where(d => !d.IsDeleted)  
                .Select(d => new DestinationViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ImageUrl = d.ImageUrl,
                    Terrain = d.Terrain.Name,
                    FavoritesCount = d.UsersDestinations.Count,
                    IsPublisher = d.PublisherId == userId,
                    IsFavorite = d.UsersDestinations.Any(ud => ud.UserId == userId)
                })
                .ToListAsync();
        }



        public async Task AddDestinationAsync(AddDestinationViewModel model, string userId)
        {
        
            var publishedOnDate = DateTime.ParseExact(model.PublishedOn, "dd-MM-yyyy", null);

          
            var destination = new Destination
            {
                Name = model.Name,
                TerrainId = model.TerrainId,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                PublishedOn = publishedOnDate,
                PublisherId = userId
            };

           
            dbContext.Destinations.Add(destination);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<TerrainViewModel>> GetAllTerrainsAsync()
        {
            return await dbContext.Terrains
                .Select(t => new TerrainViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();
        }

        public async Task<List<FavoriteDestinationViewModel>> GetUserFavoritesAsync(string userId)
        {
           
            var favorites = await dbContext.UserDestinations
                .Where(ud => ud.UserId == userId)  
                .Include(ud => ud.Destination)    
                .ThenInclude(d => d.Terrain)       
                .Where(ud => !ud.Destination.IsDeleted)  
                .ToListAsync();


            var favoriteDestinations = favorites.Select(ud => new FavoriteDestinationViewModel
            {
                Id = ud.Destination.Id,
                Name = ud.Destination.Name,
                ImageUrl = ud.Destination.ImageUrl,
                Terrain = ud.Destination.Terrain?.Name ?? "Unknown", 
         
            }).ToList();

            return favoriteDestinations ?? new List<FavoriteDestinationViewModel>();
        }

        public async Task<bool> IsFavoriteAsync(int destinationId, string userId)
        {
            return await dbContext.UserDestinations
                .AnyAsync(ud => ud.UserId == userId && ud.DestinationId == destinationId);
        }

        public async Task<Destination> GetDestinationByIdAsync(int id)
        {
            return await dbContext.Destinations
                .Include(d => d.Terrain)  
                .Include(d => d.Publisher) 
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task DeleteDestinationAsync(int destinationId)
        {
            var destination = await dbContext.Destinations.FindAsync(destinationId);

            if (destination != null)
            {
                dbContext.Destinations.Remove(destination);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task SoftDeleteDestinationAsync(int destinationId)
        {
            var destination = await dbContext.Destinations.FirstOrDefaultAsync(d => d.Id == destinationId);

            if (destination != null)
            {
                destination.IsDeleted = true; 
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateDestinationAsync(Destination destination)
        {
            var existingDestination = await dbContext.Destinations
                .FirstOrDefaultAsync(d => d.Id == destination.Id);

            if (existingDestination == null)
            {
                throw new Exception("Destination not found");
            }

            existingDestination.Name = destination.Name;
            existingDestination.Description = destination.Description;
            existingDestination.ImageUrl = destination.ImageUrl;
            existingDestination.PublishedOn = destination.PublishedOn;
            existingDestination.TerrainId = destination.TerrainId;

            await dbContext.SaveChangesAsync();
        }



        public async Task AddToFavoritesAsync(int destinationId, string userId)
        {
            var existingFavorite = await dbContext.UserDestinations
                .FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DestinationId == destinationId);

            if (existingFavorite != null)
            {
                return;
            }

            var userDestination = new UserDestination
            {
                UserId = userId,
                DestinationId = destinationId
            };

            dbContext.UserDestinations.Add(userDestination);
            await dbContext.SaveChangesAsync();
        }

        public async Task RemoveFromFavoritesAsync(int destinationId, string userId)
        {
            var userDestination = await dbContext.UserDestinations
                .FirstOrDefaultAsync(ud => ud.UserId == userId && ud.DestinationId == destinationId);

            if (userDestination != null)
            {
                dbContext.UserDestinations.Remove(userDestination);
                await dbContext.SaveChangesAsync();
            }
        }


    }
}

