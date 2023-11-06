using AutoMapper;
using PayVortex.Service.AuthAPI.Core.Models;
using PayVortex.Service.AuthAPI.DTOs;

namespace PayVortex.Service.AuthAPI.MapperProfiles
{
    public class UserRoleProfile : Profile
    {
        public UserRoleProfile()
        {
            CreateMap<RoleAssignmentRequestDto, RoleAssignmentRequest>();
        }
    }
}
