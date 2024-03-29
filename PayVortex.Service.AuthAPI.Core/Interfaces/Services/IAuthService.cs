﻿using PayVortex.Service.AuthAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayVortex.Service.AuthAPI.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
        Task<LoginResponse> Login(LoginRequest loginRequest);
        Task<UserResponse> GetUserByEmail(string email);
    }
}
