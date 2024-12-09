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

        public async Task<IActionResult> Index()
        {
            return View(await _supportTicketService.GetTicketAsync());
        }

        public async Task<IActionResult> Resolve(int id)
        {
            await _supportTicketService.ResolveTicketAsync(id);
            return RedirectToAction("Index");
        }
    }
}
