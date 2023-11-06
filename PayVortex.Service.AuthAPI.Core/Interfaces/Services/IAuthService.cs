using PayVortex.Service.AuthAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Interfaces.Services
{
    public interface IAuthService
    {
        public Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
        public Task<LoginResponse> AuthenticateAndGenerateToken(LoginRequest loginRequest);
    }
}
