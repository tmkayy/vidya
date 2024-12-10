using vidya.Web.DTOs.Discounts;

namespace vidya.Services.Data.Discounts
{
    public interface IDiscountService
    {
        decimal CalculateDiscountedPrice(decimal price, decimal percentage);

        Task AddDiscountAsync(AddDiscountDTO addDiscountDTO, int gameId);

        Task DeleteExpiredDiscountsAsync();
    }
}
