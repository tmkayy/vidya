using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Seeder
{
    public class VidyaDbSeeder:ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            if (context is null) throw new ArgumentNullException(nameof(context));

            if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

            var seeders = new List<ISeeder>
            {
                new LocationSeeder(),
                new ApplicationUserSeeder(),
                new GameSeeder(),
                new ActivationKeySeeder(),
                new LocationsKeysSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(context, serviceProvider);
            }
        }
    }
}
