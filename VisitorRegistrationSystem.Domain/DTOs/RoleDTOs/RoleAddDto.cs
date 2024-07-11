using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VisitorRegistrationSystem.Common.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.DTOs.RoleDTOs
{
    public class RoleAddDto : DtoGetBase
    {

        [DisplayName("Yetki Adı")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public string Name { get; set; }


    }
}
