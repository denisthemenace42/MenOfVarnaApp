using Men_Of_Varna.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Men_Of_Varna.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Admin")]
	public class UserManagementController : Controller
	{
		private readonly UserManager<IdentityUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public UserManagementController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public async Task<IActionResult> Index()
		{
			var users = this.userManager.Users.ToList();
			var userViewModel = new List<UserViewModel>();

            foreach (var  user in users)
            {
				var roles = await userManager.GetRolesAsync(user);
				userViewModel.Add(new UserViewModel
				{
					Id = user.Id,
					Email = user.Email,
					Roles = roles.ToList(),
				});
                
            }
            return View(userViewModel);
		}

		[HttpPost]
		public async Task<IActionResult>AssignRole(string userId, string role)
		{
			var user = await userManager.FindByIdAsync(userId);
			if (user != null && await roleManager.RoleExistsAsync(role))
			{
				await userManager.AddToRoleAsync(user, role);
			}

			return RedirectToAction("Index");
		}

        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId, string role)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user != null && await roleManager.RoleExistsAsync(role))
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                TempData["ErrorMessage"] = "User ID is required.";
                return RedirectToAction("Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    TempData["ErrorMessage"] = "Failed to delete user.";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "User not found.";
            }

            return RedirectToAction("Index");
        }



    }
}
