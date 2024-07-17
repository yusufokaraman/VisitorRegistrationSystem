using VisitorRegistrationSystem.Domain.DTOs.UserDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class UserAddAjaxViewModel
    {

        public UserAddDto UserAddDto { get; set; }

        public string UserAddPartial { get; set; }

        public UserDto UserDto { get; set; }
    }
}
