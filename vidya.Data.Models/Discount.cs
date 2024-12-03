using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Models
{
    public class Discount
    {
        [Key]
        public int Id { get; set; }

        public decimal Percentage { get; set; }

        public ICollection<Game> Games { get; set; } = new HashSet<Game>();

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
