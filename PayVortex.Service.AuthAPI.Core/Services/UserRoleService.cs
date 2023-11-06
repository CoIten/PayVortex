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
    public class UserRoleService : IUserRoleService
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AuthService> _logger;

        public UserRoleService(IAuthService authService, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, ILogger<AuthService> logger)
        {
            _authService = authService;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<RoleAssignmentResponse> AssignRole(RoleAssignmentRequest request)
        {
            try
            {
                var validationErrors = ValidateUserRoleAssignment(request);
                if (validationErrors.Any())
                {
                    return RoleAssignmentResponse.Failure("Validation Errors", validationErrors);
                }

                var userResponse = await _authService.GetUserByEmail(request.Email);
                if (!userResponse.IsSuccess)
                {
                    return RoleAssignmentResponse.Failure(userResponse.Message, userResponse.Errors);
                }

                if (!_roleManager.RoleExistsAsync(request.RoleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(request.RoleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(userResponse.User, request.RoleName);

                return RoleAssignmentResponse.Success("Role assignment was successful");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while asigning a role.");
                return RoleAssignmentResponse.Failure("An unexpected error occured", new List<string> { ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while asigning a role.");
                return RoleAssignmentResponse.Failure("An unexpected error occured", new List<string> { ex.Message });
            }
        }

        private IList<string> ValidateUserRoleAssignment(RoleAssignmentRequest request)
        {
            var validationErrors = new List<string>();

            if (string.IsNullOrEmpty(request.Email))
            {
                validationErrors.Add("Email is required");
            }

            if (string.IsNullOrEmpty(request.RoleName))
            {
                validationErrors.Add("Role name is required");
            }

            return validationErrors;
        }
    }
}
