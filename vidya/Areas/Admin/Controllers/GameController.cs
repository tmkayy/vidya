using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.ActivationKeys;
using vidya.Services.Data.Games;
using vidya.Web.DTOs.Games;

namespace vidya.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IActivationKeyService _activationKeyService;

        public GameController(IGameService gameService, IActivationKeyService activationKeyService)
        {
            _gameService = gameService;
            _activationKeyService = activationKeyService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddGameDTO addGameDTO)
        {
            if (!ModelState.IsValid)
                return View(addGameDTO);
            await _gameService.AddGameAsync(addGameDTO);
            return RedirectToAction("Index", "Game", new { area = "" });
        }
    }
}
