using VisitorRegistrationSystem.Domain.DTOs.RoleDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class RoleAddAjaxViewModel
    {
        public RoleAddDto RoleAddDto { get; set; }
        public string RoleAddPartial { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}
