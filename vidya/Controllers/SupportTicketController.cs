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
            if (this.User.GetId() is null)
            {
                return Unauthorized();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(SendTicketDTO sendTicketDTO)
        {
            string? userId = this.User.GetId();

            if (userId is null)
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }
            await _supportTicketService.SendTicketAsync(sendTicketDTO, userId);
            return RedirectToAction("Index", "Home");
        }
    }
}
