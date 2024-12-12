using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Horizons.Common.ValidationConstants;

namespace Horizons.Data.Models
{
    public class Destination
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(DestinationNameMaxLength, MinimumLength = DestinationNameMinLength)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        [Required]
        public string PublisherId { get; set; } = null!;

        [ForeignKey(nameof(PublisherId))]
        public IdentityUser Publisher { get; set; } = null!;

        [Required]
        public DateTime PublishedOn { get; set; }

        [Required]
        public int TerrainId { get; set; }

        [ForeignKey(nameof(TerrainId))]
        public Terrain Terrain { get; set; } = null!;

        public bool IsDeleted { get; set; } = false;

        public ICollection<UserDestination> UsersDestinations { get; set; } = new HashSet<UserDestination>();
    }
}
