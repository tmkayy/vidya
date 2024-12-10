using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Web.DTOs.Payments
{
    public class PaymentDTO
    {
        public string UserId { get; set; } = null!;

        public int ActivationKeyId { get; set; }
    }
}
