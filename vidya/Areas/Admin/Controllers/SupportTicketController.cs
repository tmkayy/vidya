using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.SupportTickets;

namespace vidya.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
            if (!await _supportTicketService.ExistsAsync(id))
            {
                return NotFound();
            }
            await _supportTicketService.ResolveTicketAsync(id);
            return RedirectToAction("Index");
        }
    }
}
