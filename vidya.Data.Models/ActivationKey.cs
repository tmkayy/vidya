using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Models
{
    public class ActivationKey
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Key { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Game))]
        public int GameId { get; set; }

        [Required]
        public Game Game { get; set; } = null!;

        [ForeignKey(nameof(ApplicationUser))]
        public string? UserId { get; set; }

        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<LocationsKeys> Locations { get; set; } = new HashSet<LocationsKeys>();
    }
}
