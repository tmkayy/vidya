using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;

namespace vidya.Data.Seeder
{
    public class GameSeeder : ISeeder
    {
        public async Task SeedAsync(VidyaDbContext context, IServiceProvider serviceProvider)
        {
            if (context.Games.Any())
                return;
            await context.Games.AddRangeAsync([
                new Game { Name = "fortnite", Description = "the hit game",
                    ImageUrl="https://cdn-0001.qstv.on.epicgames.com/QURZNVBFqpfNVlnlDl/image/landscape_comp.jpeg"},
                     new Game { Name = "fall guys", Description = "fell off ironically enough",
                    ImageUrl="https://cdn2.unrealengine.com/spongebobmeme-16x9-1920x1080-895841e6928c.png?resize=1&w=580"},
                     new Game { Name = "amongus", Description = "hahaha",
                    ImageUrl="https://i1.sndcdn.com/avatars-wnulRrHMNix1E4z4-BzHchQ-t240x240.jpg"},
                     new Game { Name = "star wars outlaws", Description = "the star wars game",
                    ImageUrl="https://images2.minutemediacdn.com/image/upload/c_crop,w_1920,h_1080,x_0,y_0/c_fill,w_720,ar_16:9,f_auto,q_auto,g_auto/images/voltaxMediaLibrary/mmsport/video_games/01j6cv7vexe8pf03ngvp.jpg"}
                ]);

            await context.SaveChangesAsync();
        }
    }
}
