using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.Games;
using vidya.Web.DTOs.Games;

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
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            return View(await _gameService.GetDetailGameAsync(id));
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _gameService.DeleteGame(id);
        }
    }
}
