using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Mapping;
using vidya.ThirdParty.Services.Images;
using vidya.Web.DTOs.Games;

namespace vidya.Services.Data.Games
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        private readonly ICloudinaryService _cloudinaryService;

        public GameService(IRepository<Game> gameRepository, ICloudinaryService cloudinaryService)
        {
            _gameRepository = gameRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task AddGameAsync(AddGameDTO addGameDTO)
        {
            var game = AutoMapperConfig.MapperInstance.Map<Game>(addGameDTO);
            game.ImageUrl = _cloudinaryService.UploadImage(addGameDTO.Image);
            await _gameRepository.AddAsync(game);
            await _gameRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<GameDTO>> GetGamesAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return await _gameRepository.AllAsNoTracking().To<GameDTO>().ToListAsync();
            return await _gameRepository.AllAsNoTracking()
                .Where(g => EF.Functions.Like(g.Name, $"%{name}%")).To<GameDTO>().ToListAsync();
        }
    }
}
