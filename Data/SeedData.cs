using Microsoft.AspNetCore.Identity;

namespace DisasterReliefApp.Data
{
    public static class SeedData
    {
        public static async Task SeedRolesAndAdmin(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<Models.ApplicationUser>>();

            string[] roles = new[] { "Admin", "Volunteer", "Donor", "Coordinator" };
            foreach (var r in roles)
            {
                if (!await roleManager.RoleExistsAsync(r))
                    await roleManager.CreateAsync(new IdentityRole(r));
            }

            var adminEmail = "admin@example.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);
            if (admin == null)
            {
                admin = new Models.ApplicationUser { UserName = adminEmail, Email = adminEmail, FullName = "Admin User", EmailConfirmed = true };
                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
