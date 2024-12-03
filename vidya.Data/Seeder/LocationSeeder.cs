using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;

namespace vidya.Data.Seeder
{
    public class LocationSeeder:ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            if (context.Locations.Any())
                return;
            await context.Locations.AddRangeAsync([
                new Location {Continent = "Europe" },
                new Location {Continent = "North America" },
                new Location {Continent = "South America" },
                new Location {Continent = "Asia" },
                new Location {Continent = "Australia" },
                new Location {Continent = "Africa" },
                new Location {Continent = "Antarctica" }
            ]);
                

            await context.SaveChangesAsync();
        }
    }
}
