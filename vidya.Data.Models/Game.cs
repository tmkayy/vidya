using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Description { get; set; } = null!;

        public string? ImageUrl { get; set; }

        public ICollection<ActivationKey> ActivationKeys { get; set; } = new HashSet<ActivationKey>();

        [Required]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Discount))]
        public int? DiscountId {  get; set; }

        public Discount? Discount { get; set; } 
    }
}
