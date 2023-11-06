using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthService> _logger;

        public AuthService(IAuthRepository authRepository, UserManager<User> userManager, ILogger<AuthService> logger)
        {
            _authRepository = authRepository;
            _userManager = userManager;
            _logger = logger;
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

                var user = PrepareIdentityUser(registrationRequest);

                var createdUser = await _authRepository.CreateUserAsync(user);
                return RegistrationResponse.Success("", createdUser);
            }
            catch(DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while creating a user.");
                return RegistrationResponse.Failure("Database Error", new List<string> { ex.Message });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a user.");
                return RegistrationResponse.Failure("An unexpected error occured", new List<string> { ex.Message });
            }
        }

        public Task<LoginResponse> AuthenticateAndGenerateToken(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        private IList<string> ValidateRegistrationRequest(RegistrationRequest registrationRequest)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrEmpty(registrationRequest.Name))
            {
                validationErrors.Add("Name is required");
            }

            if (string.IsNullOrEmpty(registrationRequest.LastName))
            {
                validationErrors.Add("Last name is required");
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

        private User PrepareIdentityUser(RegistrationRequest registrationRequest)
        {
            try
            {
                User user = new()
                {
                    Email = registrationRequest.Email,
                    Name = registrationRequest.Name,
                    LastName = registrationRequest.LastName,
                    UserName = registrationRequest.UserName,
                    PhoneNumber = registrationRequest.PhoneNumber,
                    CreationDate = DateTime.Now
                };

                var passwordHash = HashUserPassword(user, registrationRequest.Password);
                user.PasswordHash = passwordHash;

                var normalizedUserName = NormalizeName(user.UserName);
                user.NormalizedUserName = normalizedUserName;

                var normalizedEmail = NormalizeEmail(user.Email);
                user.NormalizedEmail = normalizedEmail;

                user.LockoutEnabled = true;

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a user.");
                throw;
            }
        }

        private string? HashUserPassword(User user, string password)
        {
            return _userManager.PasswordHasher.HashPassword(user, password);
        }

        private string? NormalizeName(string? name)
        {
            return _userManager.NormalizeName(name);
        }

        private string? NormalizeEmail(string? email)
        {
            return _userManager.NormalizeEmail(email);
        }
    }
}
