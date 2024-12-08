using vidya.Data.Models;
using vidya.Web.DTOs.ActivationKeys;

namespace vidya.Services.Data.ActivationKeys
{
    public interface IActivationKeyService
    {
        Task<IEnumerable<ActivationKeyDTO>> GetActivationKeys(int gameId);
    }
}
