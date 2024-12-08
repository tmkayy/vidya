using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Games
{
    public class GameDTO: IMapFrom<Game>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string? ImageUrl { get; set; }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal? DiscountPercentage { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Game, GameDTO>()
                .ForMember(d => d.DiscountPercentage, opt => opt.MapFrom(src => src.Discount.Percentage));
        }
    }
}
