using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Common;
using vidya.Data.Models;
using vidya.Services.Mapping;
using vidya.Web.DTOs.Locations;

namespace vidya.Web.DTOs.ActivationKeys
{
    public class AddActivationKeyDTO : IMapTo<ActivationKey>
    {
        [Required]
        [RegularExpression(GlobalConstants.ActivationKeyRegex)]
        public string Key { get; set; } = null!;
        public int GameId { get; set; }
        public IEnumerable<LocationDTO> Locations { get; set; } = new HashSet<LocationDTO>();
        [Required]
        public IEnumerable<int> SelectedIds { get; set; } = new HashSet<int>();
    }
}
