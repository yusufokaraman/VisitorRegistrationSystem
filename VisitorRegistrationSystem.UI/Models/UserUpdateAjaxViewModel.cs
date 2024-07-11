using VisitorRegistrationSystem.Domain.DTOs.UserDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class UserUpdateAjaxViewModel
    {
        //Doldurulmuş yerlerin ve hataların geriye dönmesi için
        public UserUpdateDto UserUpdateDto { get; set; }
        //Eger validate doğru değilse partial döndürür.
        public string UserUpdatePartial { get; set; }
        //Eklendikten sonra geri döner.
        public UserDto UserDto { get; set; }
    }
}
