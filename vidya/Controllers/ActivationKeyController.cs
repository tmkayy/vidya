using Microsoft.AspNetCore.Mvc;
using vidya.Services.Data.ActivationKeys;
using vidya.Services.Data.Locations;
using vidya.Web.DTOs.ActivationKeys;
using vidya.Web.Infrastructure.Extensions;

namespace vidya.Controllers
{
    public class ActivationKeyController : Controller
    {
        private readonly IActivationKeyService _activationKeyService;
        private readonly ILocationService _locationService;

        public ActivationKeyController(IActivationKeyService activationKeyService, ILocationService locationService)
        {
            _activationKeyService = activationKeyService;
            _locationService = locationService;
        }

        public async Task<IActionResult> Add(int id)
        {
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
