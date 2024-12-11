using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Mapping;
using vidya.Web.DTOs.SupportTickets;

namespace vidya.Services.Data.SupportTickets
{
    public class SupportTicketService : ISupportTicketService
    {
        private readonly IRepository<SupportTicket> _repository;

        public SupportTicketService(IRepository<SupportTicket> repository)
        {
            _repository = repository;
        }

        public async Task ResolveTicketAsync(int ticketId)
        {
            var ticket = await _repository.All().FirstOrDefaultAsync(st => st.Id == ticketId);
            ticket.IsResolved = true;
            await _repository.SaveChangesAsync();
        }

        public async Task SendTicketAsync(SendTicketDTO sendTicketDTO, string userId)
        {
            var ticket = AutoMapperConfig.MapperInstance.Map<SupportTicket>(sendTicketDTO);
            ticket.UserId = userId;
            await _repository.AddAsync(ticket);
            await _repository.SaveChangesAsync();
        }

        public async Task<SupportTicketPagedDTO> GetPagedTicketsAsync(int pageNumber, int pageSize)
        {
            int totalTickets = await _repository.AllAsNoTracking()
                            .Where(st => !st.IsResolved)
                            .CountAsync();

            var tickets = await _repository.AllAsNoTracking()
                 .Where(st => !st.IsResolved)
                 .Include(st => st.User)
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .To<TicketDTO>()
                 .ToListAsync();

            return new SupportTicketPagedDTO
            {
                Tickets = tickets,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(totalTickets / (double)pageSize),
            };
        }

        public async Task<bool> ExistsAsync(int id) => await _repository.AllAsNoTracking().AnyAsync(st => st.Id == id);
    }
}
