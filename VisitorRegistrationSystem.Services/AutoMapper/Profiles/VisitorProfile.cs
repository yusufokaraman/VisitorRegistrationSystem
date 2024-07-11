using AutoMapper;
using VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Services.AutoMapper.Profiles
{
    public class VisitorProfile : Profile
    {

        public VisitorProfile()
        {
            CreateMap<VisitorAddDto, Visitor>();

        }
    }
}
