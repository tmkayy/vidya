using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Mapping;
using vidya.Web.DTOs.ActivationKeys;

namespace vidya.Services.Data.ActivationKeys
{
    public class ActivationKeyService : IActivationKeyService
    {
        private readonly IRepository<ActivationKey> _activationKeyRepository;

        public ActivationKeyService(IRepository<ActivationKey> activationKeyRepository)
        {
            _activationKeyRepository = activationKeyRepository;
        }

        public async Task<IEnumerable<ActivationKeyDTO>> GetActivationKeys(int gameId)
        {
            return await _activationKeyRepository.AllAsNoTracking()
             .Where(ak => ak.GameId == gameId)
             .Include(ak => ak.Locations)
             .ThenInclude(ak => ak.Location)
             .To<ActivationKeyDTO>()
             .ToListAsync();
        }

        public async Task<Game> GetGameByActivationKeyAsync(int id)
        {
            var activationKey = await _activationKeyRepository.AllAsNoTracking().Include(g => g.Game)
                .FirstOrDefaultAsync(a => a.Id == id);
            return activationKey.Game;
        }
    }
}
