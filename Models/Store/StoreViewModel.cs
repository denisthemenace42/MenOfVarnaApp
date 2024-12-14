﻿namespace Men_Of_Varna.Models.Store
{
    
    public class StoreViewModel
    {
        public IEnumerable<ProductViewModel> Products { get; set; } = Enumerable.Empty<ProductViewModel>();


        // Additional properties like filtering or pagination can go here.
    }


    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public string Category { get; set; } = null!;
        public int StockQuantity { get; set; }
        public bool IsActive { get; set; }
    }



}