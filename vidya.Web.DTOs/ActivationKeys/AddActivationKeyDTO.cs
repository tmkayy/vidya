using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;
using vidya.Web.DTOs.Locations;

namespace vidya.Web.DTOs.ActivationKeys
{
    public class AddActivationKeyDTO : IMapTo<ActivationKey>
    {
        public string Key { get; set; }
        public int GameId { get; set; }
        public IEnumerable<LocationDTO> Locations { get; set; } = new HashSet<LocationDTO>();
        public IEnumerable<int> SelectedIds { get; set; } = new HashSet<int>();
    }
}
