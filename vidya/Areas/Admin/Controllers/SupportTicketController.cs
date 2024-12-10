using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.SupportTickets;

namespace vidya.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SupportTicketController : Controller
    {
        private readonly ISupportTicketService _supportTicketService;

        public SupportTicketController(ISupportTicketService supportTicketService)
        {
            _supportTicketService = supportTicketService;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 5;
            var pagedTickets = await _supportTicketService.GetPagedTicketsAsync(page, pageSize);
            return View(pagedTickets);
        }

        public async Task<IActionResult> Resolve(int id)
        {
            await _supportTicketService.ResolveTicketAsync(id);
            return RedirectToAction("Index");
        }
    }
}
