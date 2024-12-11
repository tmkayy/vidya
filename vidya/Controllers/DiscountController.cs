using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.Discounts;
using vidya.Services.Data.Games;
using vidya.Web.DTOs.Discounts;

namespace vidya.Controllers
{
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IGameService _gameService;

        public DiscountController(IDiscountService discountService, IGameService gameService)
        {
            _discountService = discountService;
            _gameService = gameService;
        }

        public IActionResult Add(int id)
        {
            if (!_gameService.ExistsAsync(id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            AddDiscountDTO addDiscountDTO = new AddDiscountDTO();
            addDiscountDTO.GameId = id;
            return View(addDiscountDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDiscountDTO addDiscountDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _discountService.AddDiscountAsync(addDiscountDTO, addDiscountDTO.GameId);
            return RedirectToAction("Index", "Game");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
