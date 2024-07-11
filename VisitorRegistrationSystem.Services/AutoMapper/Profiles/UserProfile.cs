using AutoMapper;
using VisitorRegistrationSystem.Domain.DTOs.UserDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Services.AutoMapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserAddDto, User>();
            CreateMap<User, UserUpdateDto>();
            CreateMap<UserUpdateDto, User>();
        }
    }
}
