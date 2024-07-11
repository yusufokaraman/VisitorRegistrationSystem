using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class DepartmentAddAjaxViewModel
    {
        public DepartmentAddDto DepartmentAddDto { get; set; }
        //Eger validate doğru değilse partial döndürür.
        public string DepartmentAddPartial { get; set; }
        //Eklendikten sonra geri döner.
        public DepartmentDto DepartmentDto { get; set; }
    }
}
