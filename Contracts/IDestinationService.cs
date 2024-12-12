using Men_Of_Varna.Data.Models;
using Men_Of_Varna.Models.Destinations;


namespace Men_Of_Varna.Contracts
{
    public interface IDestinationService
    {
        Task<IEnumerable<DestinationViewModel>>GetAllDestinationsAsync(string userId);

        Task AddDestinationAsync(AddDestinationViewModel model,string userId);

        Task<IEnumerable<TerrainViewModel>> GetAllTerrainsAsync();

        Task<List<FavoriteDestinationViewModel>> GetUserFavoritesAsync(string userId);

        Task<bool> IsFavoriteAsync(int destinationId, string userId);
        Task<Destination> GetDestinationByIdAsync(int id);

        Task DeleteDestinationAsync(int destinationId);

        Task SoftDeleteDestinationAsync(int destinationId);

        Task UpdateDestinationAsync(Destination destination);

        Task AddToFavoritesAsync(int destinationId, string userId);

        Task RemoveFromFavoritesAsync(int destinationId, string userId);
    }
}
