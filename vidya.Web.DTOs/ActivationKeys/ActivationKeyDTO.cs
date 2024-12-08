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
    public class ActivationKeyDTO :IHaveCustomMappings
    {
        public string Key { get; set; } = null!;

        public ICollection<string> Locations { get; set; } = new HashSet<string>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ActivationKey, ActivationKeyDTO>()
                .ForMember(d => d.Key, opt => opt.MapFrom(src => $"{src.Key[0]}{new string('*', src.Key.Length - 1)}"))
                .ForMember(d => d.Locations, opt => opt.MapFrom(src=>src.Locations.Select(x=>x.Location.Continent)));
        }
    }
}
