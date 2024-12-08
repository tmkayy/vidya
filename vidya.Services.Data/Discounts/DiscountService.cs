namespace vidya.Services.Data.Discounts
{
    public class DiscountService : IDiscountService
    {
        public decimal CalculateDiscountedPrice(decimal price, decimal percentage) => price - (price * percentage / 100.0m);
    }
}
