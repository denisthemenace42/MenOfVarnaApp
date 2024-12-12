

namespace Men_Of_Varna.Models.Destinations
{
    public class EditDestinationViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime PublishedOn { get; set; }

        public int TerrainId { get; set; }
        public IEnumerable<TerrainViewModel> Terrains { get; set; } = null!;

        public string PublisherId { get; set; } = null!;
    }


}
