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
        private readonly IRepository<LocationsKeys> _locationsKeysRepository;

        public ActivationKeyService(IRepository<ActivationKey> activationKeyRepository, IRepository<LocationsKeys> locationsKeysRepository)
        {
            _activationKeyRepository = activationKeyRepository;
            _locationsKeysRepository = locationsKeysRepository;
        }

        public async Task AddActivationKeyAsync(AddActivationKeyDTO addActivationKeyDTO)
        {
            var activationKey = AutoMapperConfig.MapperInstance.Map<ActivationKey>(addActivationKeyDTO);
            await _activationKeyRepository.AddAsync(activationKey);
            await _activationKeyRepository.SaveChangesAsync();

            // locations
            foreach (var id in addActivationKeyDTO.SelectedIds)
            {
                await _locationsKeysRepository.AddAsync(new LocationsKeys
                {
                    ActivationKey = activationKey,
                    LocationId = id
                });
            }
            await _locationsKeysRepository.SaveChangesAsync();
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
    }
}
