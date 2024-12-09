using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Mapping;
using vidya.Web.DTOs.Locations;

namespace vidya.Services.Data.Locations
{
    public class LocationService : ILocationService
    {
        private readonly IRepository<Location> _locationRepository;

        public LocationService(IRepository<Location> locationRepository)
        {
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<LocationDTO>> GetLocationsAsync()
        {
            return await _locationRepository.AllAsNoTracking().To<LocationDTO>().ToListAsync();
        }
    }
}
