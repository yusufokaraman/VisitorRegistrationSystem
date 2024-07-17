using VisitorRegistrationSystem.Domain.DTOs.UserDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class UserUpdateAjaxViewModel
    {

        public UserUpdateDto UserUpdateDto { get; set; }

        public string UserUpdatePartial { get; set; }

        public UserDto UserDto { get; set; }
    }
}
