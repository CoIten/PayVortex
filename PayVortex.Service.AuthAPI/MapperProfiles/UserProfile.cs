﻿using AutoMapper;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.DTOs;

namespace PayVortex.Service.AuthAPI.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationRequestDto, RegistrationRequest>();
            CreateMap<RegistrationRequest, RegistrationRequestDto>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<LoginRequestDto, LoginRequest>();
        }
    }
}
