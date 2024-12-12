using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Men_Of_Varna.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Men_Of_Varna.Models.Destinations
{
    public class AddDestinationViewModel
    {
        [Required(ErrorMessage = "Destination name is required.")]
        [StringLength(ValidationConstants.DestinationNameMaxLength,
                      MinimumLength = ValidationConstants.DestinationNameMinLength,
                      ErrorMessage = "Destination name must be between {2} and {1} characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Please select a terrain.")]
        public int TerrainId { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(ValidationConstants.DescriptionMaxLength,
                      MinimumLength = ValidationConstants.DescriptionMinLength,
                      ErrorMessage = "Description must be between {2} and {1} characters.")]
        public string Description { get; set; } = null!;

        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Published date is required.")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public string PublishedOn { get; set; } = DateTime.Now.ToString("dd-MM-yyyy");

        [ValidateNever]
        public IEnumerable<TerrainViewModel> Terrains { get; set; } = null!;
    }

    public class TerrainViewModel
    {
        public int Id { get; set; }

        [StringLength(ValidationConstants.TerrainNameMaxLength,
                      MinimumLength = ValidationConstants.TerrainNameMinLength,
                      ErrorMessage = "Terrain name must be between {2} and {1} characters.")]
        public string Name { get; set; } = null!;
    }
}
