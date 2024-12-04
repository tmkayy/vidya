using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;

namespace vidya.Data.Seeder
{
    public class LocationsKeysSeeder : ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            if (context.LocationsKeys.Any())
                return;

            var keyLocations = new List<LocationsKeys>();
            int count = context.ActivationKeys.Count();
            for (int i = 1; i <= count; i++)
            {
                int locId = new Random().Next(1, 8);
                for (int j = 1; j <= locId; j++)
                {
                    int locationId = new Random().Next(1, 8);
                    if (!keyLocations.Any(x => x.LocationId == locationId && x.KeyId == i))
                    {
                        keyLocations.Add(new LocationsKeys { KeyId = i, LocationId = locationId });
                    }
                }
            }

            await context.LocationsKeys.AddRangeAsync(keyLocations);

            await context.SaveChangesAsync();
        }
    }
}
