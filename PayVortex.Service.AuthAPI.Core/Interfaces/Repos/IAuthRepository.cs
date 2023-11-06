using PayVortex.Service.AuthAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Interfaces.Repos
{
    public interface IAuthRepository
    {
        Task<User> CreateUserAsync(User user);
        Task<User> GetUserByUserName(string normalizedUserName);
        Task<User> GetUserByEmail(string normalizedEmail);
    }
}
