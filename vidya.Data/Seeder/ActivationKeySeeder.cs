using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Seeder.Fakers;

namespace vidya.Data.Seeder
{
    public class ActivationKeySeeder : ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            if (context.ActivationKeys.Any())
            {
                return;
            }
            var applicationKeyFaker = new ActivationKeyFaker(context);
            await context.ActivationKeys.AddRangeAsync(applicationKeyFaker.Generate(16));
            await context.SaveChangesAsync();
        }
    }
}
