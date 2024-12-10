using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Web.DTOs.Payments;

namespace vidya.ThirdParty.Services.Payments
{
    public interface IPaymentService
    {
        string GetSessionUrl(int activationKeyId);

        Task PayAsync(PaymentDTO paymentDTO);

    }
}
