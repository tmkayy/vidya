using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Web.DTOs.SupportTickets;

namespace vidya.Services.Data.SupportTickets
{
    public interface ISupportTicketService
    {
        Task SendTicketAsync(SendTicketDTO sendTicketDTO, string userId);

        Task<IEnumerable<TicketDTO>> GetTicketAsync();

        Task ResolveTicketAsync(int ticketId);
    }
}
