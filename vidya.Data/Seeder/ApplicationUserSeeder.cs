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
            var applicationUserFaker = new ApplicationUserFaker();
            foreach (var user in applicationUserFaker.Generate(20))
            {
                await userManager.CreateAsync(user, "Parola!123");
            }
        }
    }
}
