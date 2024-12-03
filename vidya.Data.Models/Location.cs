using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Continent { get; set; } = null!;

        public ICollection<LocationsKeys> Locations { get; set; } =  new HashSet<LocationsKeys>();
    }
}
