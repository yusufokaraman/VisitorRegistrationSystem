using AutoMapper;
using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Services.AutoMapper.Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentAddDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
            CreateMap<Department, DepartmentUpdateDto>();
        }
    }
}
