using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
        {
            try
            {
                var validationErrors = ValidateRegistrationRequest(registrationRequest);
                if (validationErrors.Any())
                {
                    return RegistrationResponse.Failure("Validation Errors", validationErrors);
                }

                User user = new()
                {
                    Email = registrationRequest.Email,
                    Name = registrationRequest.Name,
                    PhoneNumber = registrationRequest.PhoneNumber
                };

                var passwordHash = _userManager.PasswordHasher.HashPassword(user, registrationRequest.Password);
                user.PasswordHash = passwordHash;

                var createdUser = await _authRepository.CreateUserAsync(user);
                return RegistrationResponse.Success("", createdUser);
            }
            catch(DbUpdateException ex)
            {
                return RegistrationResponse.Failure("Database Error", new List<string> { ex.Message });
            }
            catch(Exception ex)
            {
                return RegistrationResponse.Failure("An unexpected error occured", new List<string> { ex.Message });
            }
        }

        private IList<string> ValidateRegistrationRequest(RegistrationRequest registrationRequest)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrEmpty(registrationRequest.Name))
            {
                validationErrors.Add("Name is required");
            }

            if (string.IsNullOrEmpty(registrationRequest.Email))
            {
                validationErrors.Add("Email is required.");
            }

            if (string.IsNullOrEmpty(registrationRequest.Password))
            {
                validationErrors.Add("Password is required.");
            }

            return validationErrors;
        }
    }
}
