using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs
{
    public class DepartmentListDto : DtoGetBase
    {
        public IList<Department> Departments { get; set; }
    }
}
