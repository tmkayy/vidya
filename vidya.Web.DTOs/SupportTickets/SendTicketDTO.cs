using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.SupportTickets
{
    public class SendTicketDTO : IMapTo<SupportTicket>
    {
        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;
    }
}
