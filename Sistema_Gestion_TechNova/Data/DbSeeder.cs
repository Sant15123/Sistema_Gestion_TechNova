using Microsoft.AspNetCore.Identity;
using Sistema_Gestion_TechNova.Models;

namespace Sistema_Gestion_TechNova.Data
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

            // Rol Admin
            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new ApplicationRole("Admin"));

            // Usuario Admin
            string adminEmail = "admin@technova.com";
            var admin = await userManager.FindByEmailAsync(adminEmail);

            if (admin == null)
            {
                admin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(admin, "Admin123!");
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}
