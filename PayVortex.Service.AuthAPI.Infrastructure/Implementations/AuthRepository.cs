using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PayVortex.Service.AuthAPI.Core.Interfaces.Repos;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Infrastructure.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger<AuthRepository> _logger;
        public AuthRepository(AppDbContext appDbContext, ILogger<AuthRepository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            try
            {
                await _appDbContext.Users.AddAsync(user);
                await _appDbContext.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Database error occurred while creating a user.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a user.");
                throw;
            }
        }

        public async Task<User> GetUserByUserName(string normalizedUserName)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName);
                return user;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Database error occurred while retrieving a user.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a user.");
                throw;
            }
        }

        public async Task<User?> GetUserByEmail(string normalizedEmail)
        {
            try
            {
                var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
                return user;
            }
            catch (DbException ex)
            {
                _logger.LogError(ex, "Database error occurred while retrieving a user.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving a user.");
                throw;
            }
        }

    }
}
