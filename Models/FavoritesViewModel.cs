namespace Horizons.Models
{
    public class FavoritesViewModel
    {
        public List<FavoriteDestinationViewModel> Destinations { get; set; } = null!;
    }

    public class FavoriteDestinationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Terrain { get; set; } = null!;
    }
}
