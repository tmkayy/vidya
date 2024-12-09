using Microsoft.EntityFrameworkCore;
using vidya.Data.Models;
using vidya.Data.Repositories;
using vidya.Services.Mapping;
using vidya.Web.DTOs.Discounts;

namespace vidya.Services.Data.Discounts
{
    public class DiscountService : IDiscountService
    {

        private readonly IRepository<Discount> _repositoryDiscount;
        private readonly IRepository<Game> _repositoryGame;

        public DiscountService(IRepository<Discount> repositoryDiscount, IRepository<Game> repositoryGame)
        {
            _repositoryDiscount = repositoryDiscount;
            _repositoryGame = repositoryGame;
        }

        public async Task AddDiscountAsync(AddDiscountDTO addDiscountDTO, int gameId)
        {
            var game = await _repositoryGame.AllAsNoTracking().FirstOrDefaultAsync(g => g.Id == gameId);
            if (game == null)
            {
                throw new ArgumentException("Invalid game ID.");
            }
            var existingDiscount = await _repositoryDiscount
                .All()
                .Include(d => d.Games) 
                .FirstOrDefaultAsync(d => d.Id == addDiscountDTO.Id);

            if (existingDiscount != null)
            {
                if (!existingDiscount.Games.Any(g => g.Id == gameId))
                {
                    existingDiscount.Games.Add(game);
                }
            }
            else
            {
                var newDiscount = AutoMapperConfig.MapperInstance.Map<Discount>(addDiscountDTO);
                newDiscount.Games = new List<Game> { game };

                _repositoryDiscount.Update(newDiscount);
            }
            await _repositoryDiscount.SaveChangesAsync();
        }


        public decimal CalculateDiscountedPrice(decimal price, decimal percentage) => price - (price * percentage / 100.0m);
    }
}
