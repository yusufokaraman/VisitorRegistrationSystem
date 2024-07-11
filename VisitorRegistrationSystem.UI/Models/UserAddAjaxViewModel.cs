using VisitorRegistrationSystem.Domain.DTOs.UserDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class UserAddAjaxViewModel
    {
        //Doldurulmuş yerlerin ve hataların geriye dönmesi için
        public UserAddDto UserAddDto { get; set; }
        //Eger validate doğru değilse partial döndürür.
        public string UserAddPartial { get; set; }
        //Eklendikten sonra geri döner.
        public UserDto UserDto { get; set; }
    }
}
