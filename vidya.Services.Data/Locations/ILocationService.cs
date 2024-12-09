using vidya.Web.DTOs.Locations;

namespace vidya.Services.Data.Locations
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetLocationsAsync();
    }
}
