using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.RoleDTOs
{
    public class RoleListDto : DtoGetBase
    {
        public IList<Role> Roles { get; set; }
    }
}
