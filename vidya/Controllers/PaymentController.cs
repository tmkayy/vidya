using Microsoft.AspNetCore.Mvc;
using vidya.ThirdParty.Services.Payments;

namespace vidya.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult Pay(int id)
        {
            string sessionUrl = _paymentService.GetSessionUrl(id);
            return sessionUrl is null ? BadRequest() : Redirect(sessionUrl);
        }
    }
}
