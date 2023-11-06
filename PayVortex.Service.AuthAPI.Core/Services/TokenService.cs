using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PayVortex.Service.AuthAPI.Core.Interfaces.Services;
using PayVortex.Service.AuthAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly ILogger<TokenService> _logger;
        public TokenService(IOptions<JwtOptions> jwtOptions, ILogger<TokenService> logger)
        {
            _jwtOptions = jwtOptions.Value;
            _logger = logger;
        }

        public TokenResponse GenerateToken(User user)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var claims = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var expires = DateTime.Now.AddHours(1);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Audience = _jwtOptions.Audience,
                    Issuer = _jwtOptions.Issuer,
                    Subject = new ClaimsIdentity(claims),
                    Expires = expires,
                    SigningCredentials = creds
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return TokenResponse.Success("", tokenHandler.WriteToken(token));
            }
            catch (SecurityTokenException ex)
            {
                _logger.LogError(ex, "Error occured during token generation");
                return TokenResponse.Failure("An unexpected error occured", new List<string>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occured during token generation");
                return TokenResponse.Failure("An unexpected error occured", new List<string>());
            }
        }
    }
}
