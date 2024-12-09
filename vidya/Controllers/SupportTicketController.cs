using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.SupportTickets;
using vidya.Web.DTOs.SupportTickets;
using vidya.Web.Infrastructure.Extensions;

namespace vidya.Controllers
{
    public class SupportTicketController : Controller
    {
        private readonly ISupportTicketService _supportTicketService;

        public SupportTicketController(ISupportTicketService supportTicketService)
        {
            _supportTicketService = supportTicketService;
        }

        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendTicketDTO sendTicketDTO)
        {
            await _supportTicketService.SendTicketAsync(sendTicketDTO, this.User.GetId());
            return RedirectToAction("Index", "Home");
        }
    }
}
