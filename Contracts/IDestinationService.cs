using Horizons.Data.Models;
using Horizons.Models;


namespace Horizons.Contracts
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
