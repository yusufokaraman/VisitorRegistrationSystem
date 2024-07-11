using AutoMapper;
using VisitorRegistrationSystem.Domain.DTOs.RoleDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Services.AutoMapper.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleAddDto, Role>();

        }
    }
}
