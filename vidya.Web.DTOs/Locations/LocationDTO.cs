using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.Locations
{
    public class LocationDTO : IMapFrom<Location>
    {
        public int Id { get; set; }
        public string Continent { get; set; }
    }
}
