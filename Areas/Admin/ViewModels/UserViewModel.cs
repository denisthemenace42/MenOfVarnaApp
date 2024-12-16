namespace Men_Of_Varna.Areas.Admin.ViewModels
{
	public class UserViewModel
	{
        public string Id { get; set; } = string.Empty; // Change Guid to string
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
