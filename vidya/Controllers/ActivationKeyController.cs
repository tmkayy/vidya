using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.ActivationKeys;
using vidya.Services.Data.Games;
using vidya.Services.Data.Locations;
using vidya.Web.DTOs.ActivationKeys;
using vidya.Web.Infrastructure.Extensions;

namespace vidya.Controllers
{
    public class ActivationKeyController : Controller
    {
        private readonly IActivationKeyService _activationKeyService;
        private readonly IGameService _gameService;
        private readonly ILocationService _locationService;

        public ActivationKeyController(IActivationKeyService activationKeyService, IGameService gameService, ILocationService locationService)
        {
            _activationKeyService = activationKeyService;
            _gameService = gameService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Add(int id)
        {
            if (!await _gameService.ExistsAsync(id))
            {
                return NotFound();
            }
            var dto = new AddActivationKeyDTO
            {
                GameId = id,
                Locations = await _locationService.GetLocationsAsync()
            };
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddActivationKeyDTO addActivationKeyDTO)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            await _activationKeyService.AddActivationKeyAsync(addActivationKeyDTO);
            return RedirectToAction("Details", "Game", new { id = addActivationKeyDTO.GameId });
        }

        public async Task<IActionResult> Bought()
        {
            string? userId = this.User.GetId();

            if (userId is null)
            {
                return Unauthorized();
            }

            return View(await _activationKeyService.GetBoughtKeysAsync(userId));
        }
    }
}
