using Microsoft.AspNetCore.Identity;
using PayVortex.Service.AuthAPI.Core.Interfaces.Repos;
using PayVortex.Service.AuthAPI.Core.Interfaces.Services;
using PayVortex.Service.AuthAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<User> _userManager;

        public AuthService(IAuthRepository authRepository, UserManager<User> userManager)
        {
            _authRepository = authRepository;
            _userManager = userManager;
        }

        public async Task<User> Register(RegistrationRequest registrationRequest)
        {
            User user = new()
            {
                Email = registrationRequest.Email,
                Name = registrationRequest.Name,
                PhoneNumber = registrationRequest.PhoneNumber
            };

            try
            {
                var passwordHash = _userManager.PasswordHasher.HashPassword(user, registrationRequest.Password);
                user.PasswordHash = passwordHash;

                var createdUser = await _authRepository.CreateUserAsync(user);
                return createdUser;
            }
            catch(Exception)
            {

            }
        }
    }
}
