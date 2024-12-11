using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.ActivationKeys;
using vidya.Services.Data.Games;
using vidya.Web.DTOs.Games;

namespace vidya.Controllers
{
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IActivationKeyService _activationKeyService;

        public GameController(IGameService gameService, IActivationKeyService activationKeyService)
        {
            _gameService = gameService;
            _activationKeyService = activationKeyService;
        }

        public async Task<IActionResult> Index(string name = "")
        {
            ViewData["name"] = name;
            return View(await _gameService.GetGamesAsync(name));
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await _gameService.ExistsAsync(id))
            {
                return NotFound();
            }
            var details = await _gameService.GetDetailGameAsync(id);
            details.Keys = await _activationKeyService.GetActivationKeys(id);

            return View(details);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _gameService.DeleteGame(id);
        }
    }
}
