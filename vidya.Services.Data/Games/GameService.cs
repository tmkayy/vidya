using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Web.DTOs.Games;

namespace vidya.Services.Data.Games
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        public GameService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<IEnumerable<GameDTO>> GetGamesAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await _gameRepository.AllAsNoTracking().ToListAsync();
        }
    }
}
