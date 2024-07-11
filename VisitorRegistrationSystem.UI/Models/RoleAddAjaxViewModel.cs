using VisitorRegistrationSystem.Domain.DTOs.RoleDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class RoleAddAjaxViewModel
    {
        public RoleAddDto RoleAddDto { get; set; }
        //Eger validate doğru değilse partial döndürür.
        public string RoleAddPartial { get; set; }
        //Eklendikten sonra geri döner.
        public RoleDto RoleDto { get; set; }
    }
}
