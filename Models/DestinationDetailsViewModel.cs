﻿using System;

namespace Horizons.ViewModels
{
    public class DestinationDetailsViewModel
    {
       
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Terrain { get; set; } = null!;    
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; } = null!;

        public bool IsPublisher { get; set; }
        public bool IsFavorite { get; set; }
    }
}
