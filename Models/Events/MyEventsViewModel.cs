
namespace Men_Of_Varna.Models.Events
{
    public class MyEventsViewModel
    {
        public List<MyEventViewModel> Events { get; set; } = null!;
    }

    public class MyEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string PictureUrl { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
