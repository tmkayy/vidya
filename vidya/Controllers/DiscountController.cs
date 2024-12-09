using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.Discounts;
using vidya.Web.DTOs.Discounts;

namespace vidya.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        public IActionResult Add(int id)
        {
            AddDiscountDTO addDiscountDTO = new AddDiscountDTO();
            addDiscountDTO.GameId = id;
            return View(addDiscountDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDiscountDTO addDiscountDTO)
        {
            await _discountService.AddDiscountAsync(addDiscountDTO, addDiscountDTO.GameId);
            return RedirectToAction("Index", "Game");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
