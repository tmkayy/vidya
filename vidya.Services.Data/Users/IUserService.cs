using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vidya.Services.Data.Users
{
    public interface IUserService
    {
        Task<string> GetUserIdByEmail(string email);
    }
}
