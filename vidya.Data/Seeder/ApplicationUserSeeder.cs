using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Data.Seeder.Fakers;

namespace vidya.Data.Seeder
{
    public class ApplicationUserSeeder : ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (userManager.Users.Any()) return;

            var applicationUserFaker = new ApplicationUserFaker();
            foreach (var user in applicationUserFaker.Generate(20))
            {
                await userManager.CreateAsync(user, "Parola!123");
            }

            await SeedAdminAsync(userManager);
        }

        private async Task SeedAdminAsync(UserManager<ApplicationUser> userManager)
        {
            var adminToCreate = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
            };

            var admin = await userManager.CreateAsync(adminToCreate, "Parola!123");

            if (admin.Succeeded)
            {
                await userManager.AddToRoleAsync(adminToCreate, "Admin");
            }
        }
    }
}
