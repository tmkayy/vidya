using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.ThirdParty.Services.Payments
{
    public interface IPaymentService
    {
        string GetSessionUrl(int activationKeyId);

    }
}
