using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MOV.Common
{
    public static class DatabaseSeeder
    {
        public static void SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new[] { "Admin", "User" };

            foreach (var role in roles)
            {
                var roleExist = roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                var result = roleManager.CreateAsync(new IdentityRole { Name = role}).GetAwaiter().GetResult();
                if (!roleExist)
                {
                    throw new Exception($"Failed to create role: {role}");
                }
            }
        }

        public static void AssignAdminRole(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            string adminEmail = "admin@example.com";
            string adminPassword = "Admin@123";

            var adminUser = userManager.FindByEmailAsync(adminEmail).GetAwaiter().GetResult();
            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var createUserResult = userManager.CreateAsync(adminUser, adminPassword).GetAwaiter().GetResult();
                if (!createUserResult.Succeeded)
                {
                    throw new Exception($"Failed to create admin user: {adminEmail}");
                }
            }

            var isInRole = userManager.IsInRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
            if (!isInRole)
            {
                var addRoleResult = userManager.AddToRoleAsync(adminUser, "Admin").GetAwaiter().GetResult();
                if (!addRoleResult.Succeeded)
                {
                    throw new Exception($"Failed to assign admin role to user: {adminEmail}");
                }
            }
        }

    }

}
