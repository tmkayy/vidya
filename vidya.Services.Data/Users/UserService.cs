using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vidya.Data.Models;
using vidya.Data.Repositories;

namespace vidya.Services.Data.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> _userRepository;

        public UserService(IRepository<ApplicationUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> GetUserIdByEmail(string email)
        {
            var user = await _userRepository.AllAsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return user.Id;
        }
    }
}
