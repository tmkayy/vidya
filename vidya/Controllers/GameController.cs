using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.Games;

namespace vidya.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task<IActionResult> Index(string name = "")
        {
            ViewData["name"] = name;
            return View(await _gameService.GetGamesAsync(name));
        }
    }
}
