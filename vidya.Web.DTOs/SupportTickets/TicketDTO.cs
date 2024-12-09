using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Services.Mapping;

namespace vidya.Web.DTOs.SupportTickets
{
    public class TicketDTO:IMapFrom<SupportTicket>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Content { get; set; } = null!;

        public string Email { get; set; } = null!;

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<SupportTicket, TicketDTO>().ForMember(d=>d.Email, opt=>opt.MapFrom(src=>src.User.Email));
        }
    }
}
