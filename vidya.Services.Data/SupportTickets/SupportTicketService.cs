using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<TicketDTO>> GetTicketAsync()
        {
            return await _repository.AllAsNoTracking().Where(st => !st.IsResolved)
                .Include(st => st.User).To<TicketDTO>().ToListAsync();
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
    }
}
