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

        public Task<IEnumerable<SendTicketDTO>> GetTicketAsync()
        {
            
        }

        public Task ResolveTicketAsync(int ticketId)
        {
            throw new NotImplementedException();
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
