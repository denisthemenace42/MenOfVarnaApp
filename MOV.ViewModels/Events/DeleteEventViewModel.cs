namespace MOV.ViewModels.Events
{
    public class DeleteEventViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CreatedBy { get; set; } = null!;

    }
}
