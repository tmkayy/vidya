using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Seeder
{
    public interface ISeeder
    {
        Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider);
    }
}
