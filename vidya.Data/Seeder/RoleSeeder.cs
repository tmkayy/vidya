using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace vidya.Data.Seeder
{
    public class RoleSeeder : ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var seedRoleAsync = async (string name) =>
            {
                if (await roleManager.FindByNameAsync(name) is null)
                {
                    await roleManager.CreateAsync(new IdentityRole(name));
                }
            };

            await seedRoleAsync("Admin");
        }
    }
}
