using MOV.ViewModels.Events;

namespace MOV.ViewModels.Store
{
    public class ProductDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = null!;
        public int StockQuantity { get; set; }
        public string Category { get; set; } = null!;
        public bool IsActive { get; set; }

        public List<CommentViewModel> Comments { get; set; } = new();
    }

    public class CommentViewModel
    {
        public string Author { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string Content { get; set; } = null!;
    }
}
