using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs
{
    public class DepartmentDto : DtoGetBase
    {
        public Department Department { get; set; }
    }
}
