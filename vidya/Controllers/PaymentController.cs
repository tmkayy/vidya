using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using vidya.Services.Data.ActivationKeys;
using vidya.Services.Data.Users;
using vidya.ThirdParty.Services.Payments;
using vidya.Web.DTOs.Payments;

namespace vidya.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IActivationKeyService _activationKeyService;

        public PaymentController(IPaymentService paymentService, IConfiguration configuration, IUserService userService, IActivationKeyService activationKeyService)
        {
            _paymentService = paymentService;
            _configuration = configuration;
            _userService = userService;
            _activationKeyService = activationKeyService;
        }

        [HttpGet]
        public IActionResult Pay(int id)
        {
            if (!_activationKeyService.ExistsAsync(id).GetAwaiter().GetResult())
            {
                return NotFound();
            }
            string sessionUrl = _paymentService.GetSessionUrl(id);
            return sessionUrl is null ? BadRequest() : Redirect(sessionUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(
                 json,
                 Request.Headers["Stripe-Signature"],
                 _configuration["Stripe:WHSecret"],
                 throwOnApiVersionMismatch: false
                );

                if (stripeEvent.Type == "checkout.session.completed")
                {
                    var session = stripeEvent.Data.Object as Session;
                    var paymentDTO = new PaymentDTO
                    {
                        ActivationKeyId = int.Parse(session.Metadata["id"]),
                        UserId = await _userService.GetUserIdByEmail(session.CustomerDetails.Email)
                    };
                    await _paymentService.PayAsync(paymentDTO);
                }
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                Console.WriteLine(e.StripeError.Message);
                return BadRequest();
            }
        }
    }
}
