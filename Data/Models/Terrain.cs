using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Men_Of_Varna.Common.ValidationConstants;

namespace Men_Of_Varna.Data.Models
{
    public class Terrain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(TerrainNameMaxLength, MinimumLength = TerrainNameMinLength)]
        public string Name { get; set; } = null!;

        public ICollection<Destination> Destinations { get; set; } = new HashSet<Destination>();
    }
}
