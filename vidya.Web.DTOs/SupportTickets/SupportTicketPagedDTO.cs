using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Web.DTOs.SupportTickets
{
    public class SupportTicketPagedDTO
    {
        public IEnumerable<TicketDTO> Tickets { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
