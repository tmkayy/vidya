using vidya.Web.DTOs.SupportTickets;

namespace vidya.Services.Data.SupportTickets
{
    public interface ISupportTicketService
    {
        Task SendTicketAsync(SendTicketDTO sendTicketDTO, string userId);

        Task<SupportTicketPagedDTO> GetPagedTicketsAsync(int pageNumber, int pageSize);

        Task ResolveTicketAsync(int ticketId);
    }
}
