using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.RoleDTOs
{
    public class RoleDto : DtoGetBase
    {
        public Role Role { get; set; }
    }
}
