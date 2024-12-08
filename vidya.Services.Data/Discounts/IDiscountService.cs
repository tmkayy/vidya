namespace vidya.Services.Data.Discounts
{
    public interface IDiscountService
    {
        decimal CalculateDiscountedPrice(decimal price, decimal percentage);
    }
}
