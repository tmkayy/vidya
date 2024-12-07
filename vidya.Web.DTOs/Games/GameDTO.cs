using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Games
{
    public class GameDTO: IMapFrom<Game>
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }
    }
}
