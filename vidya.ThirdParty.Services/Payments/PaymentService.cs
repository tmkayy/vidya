using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using vidya.Data.Models;
using vidya.Data.Repositories;

namespace vidya.ThirdParty.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IRepository<ActivationKey> _activationKeyRepository;

        public PaymentService(IConfiguration configuration, IRepository<ActivationKey> activationKeyRepository)
        {
            _configuration = configuration;
            _activationKeyRepository = activationKeyRepository;
        }

        public string GetSessionUrl(int activationKeyId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];

            var game = GetGameByActivationKeyAsync(activationKeyId).GetAwaiter().GetResult();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = ["card"],
                Metadata = new Dictionary<string, string>()
                {
                    { "id", activationKeyId.ToString() }
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmountDecimal = CalculateDiscountedPrice(game),
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = game.Name,
                                Images = [game.ImageUrl]
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "https://localhost:7255/Game",
                CancelUrl = "https://localhost:7255/Game",
            };

            var sessionService = new SessionService();
            var session = sessionService.Create(options);

            return session.Url;
        }

        private async Task<Game> GetGameByActivationKeyAsync(int id)
        {
            var activationKey = await _activationKeyRepository.AllAsNoTracking().Include(g => g.Game).ThenInclude(g => g.Discount)
                .FirstOrDefaultAsync(a => a.Id == id);
            return activationKey.Game;
        }

        private decimal CalculateDiscountedPrice(Game game)
        {
            decimal discountedPrice = game.Price;
            if (game.Discount is not null && game.Discount.Percentage > 0)
            {
                discountedPrice = (game.Price - (game.Price * game.Discount.Percentage / 100.0m)) * 100;
            }
            return discountedPrice;
        }
    }
}
