using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Common;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.SupportTickets
{
    public class SendTicketDTO : IMapTo<SupportTicket>
    {
        [MaxLength(GlobalConstants.SupportTicketTitleMaxLength)]
        [MinLength(GlobalConstants.SupportTicketTitleMinLength)]
        public string Title { get; set; } = null!;

        [MaxLength(GlobalConstants.SupportTicketContentMaxLength)]
        [MinLength(GlobalConstants.SupportTicketContentMinLength)]
        public string Content { get; set; } = null!;
    }
}
