using Microsoft.Extensions.Configuration;
using Stripe;
using Stripe.Checkout;
using vidya.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.ThirdParty.Services.Payments
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;

        public PaymentService(IConfiguration configuration, IActivationKeyService)
        {
            _configuration = configuration;
        }

        public string GetSessionUrl(int activationKeyId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:SecretKey"];
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
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
                            UnitAmountDecimal = paymentDTO.TotalPrice * 100,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = paymentDTO.Model,
                                Images = new List<string?> { paymentDTO.ImageUrl },
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment",
                SuccessUrl = "http://localhost:4200/success",
                CancelUrl = "http://localhost:4200/cancel",
            };

            var sessionService = new SessionService();
            var session = sessionService.Create(options);

            return Ok(new { url = session.Url });
        }
    }
}
