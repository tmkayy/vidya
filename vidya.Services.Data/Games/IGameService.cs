using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Web.DTOs.Games;

namespace vidya.Services.Data.Games
{
    public interface IGameService
    {
        Task<IEnumerable<GameDTO>> GetGamesAsync(string name = "");

    }
}
