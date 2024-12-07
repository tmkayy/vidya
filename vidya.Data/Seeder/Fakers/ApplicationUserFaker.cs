using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;

namespace vidya.Data.Seeder.Fakers
{
    public class ApplicationUserFaker : Faker<ApplicationUser>
    {
        public ApplicationUserFaker()
        {
            RuleFor(au => au.UserName, f => f.Person.Email);
            RuleFor(au => au.Email, f => f.Person.Email);
            RuleFor(au => au.PhoneNumber, f => f.Person.Phone);
        }
    }
}
