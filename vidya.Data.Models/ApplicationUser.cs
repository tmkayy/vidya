using Microsoft.AspNetCore.Identity;

namespace vidya.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ActivationKey> ActivationKeys { get; set; } = new HashSet<ActivationKey>();

        public ICollection<SupportTicket> SupportTickets { get; set; } = new HashSet<SupportTicket>();
    }
}
