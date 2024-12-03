using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using vidya.Data.Models;

namespace vidya.Data
{
    public class VidyaDbContext : IdentityDbContext<ApplicationUser>
    {
        public VidyaDbContext(DbContextOptions<VidyaDbContext> options) : base(options)
        {
            
        }

        public DbSet<ActivationKey> ActivationKeys { get; set; }

        public DbSet<Discount> Discounts { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<LocationsKeys> LocationsKeys { get; set; }

        public DbSet<SupportTicket> SupportTickets { get; set; }
    }
}
