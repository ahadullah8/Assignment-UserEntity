using Assignment_UserEntity.Models;
using Microsoft.AspNetCore.Identity;

namespace Assignment_UserEntity.Data.Seeding
{
    public static class SeedDefaultUser
    {
        public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
        {
            if (await userManager.FindByEmailAsync("admin@example.com") == null)
            {
                var user = new AppUser
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FullName = "AdminUser"
                };
                await userManager.CreateAsync(user,"Aa@11111");
            }
        }
    }
}
