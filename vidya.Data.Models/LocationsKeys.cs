using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Models
{
    [PrimaryKey(nameof(KeyId), nameof(LocationId))]
    public class LocationsKeys
    {
        [Required]
        [ForeignKey(nameof(ActivationKey))]
        public int KeyId { get; set; }

        public ActivationKey ActivationKey { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Location))]
        public int LocationId { get; set; }

        public Location Location { get; set; } = null!;
    }
}
