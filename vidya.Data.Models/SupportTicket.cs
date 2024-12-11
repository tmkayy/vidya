using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vidya.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Data.Models
{
    public class SupportTicket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SupportTicketTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(GlobalConstants.SupportTicketContentMaxLength)]
        public string Content { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;

        public bool IsResolved { get; set; } = false;
    }
}
