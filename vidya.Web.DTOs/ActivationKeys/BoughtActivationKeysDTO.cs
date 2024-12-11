using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.ActivationKeys
{
    public class BoughtActivationKeysDTO : IMapFrom<ActivationKey>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ActivationKey, BoughtActivationKeysDTO>()
                .ForMember(dest => dest.GamePicture, opt => opt.MapFrom(src => src.Game.ImageUrl))
                .ForMember(dest => dest.GameName, opt => opt.MapFrom(src => src.Game.Name))
                .ForMember(dest => dest.Locations, opt => opt.MapFrom(src => src.Locations.Select(l => l.Location.Continent)));
        }

        public string Key { get; set; } = null!;

        public ICollection<string> Locations { get; set; } = new HashSet<string>();

        public string? GamePicture { get; set; }

        public string GameName { get; set; } = null!;
    }
}
