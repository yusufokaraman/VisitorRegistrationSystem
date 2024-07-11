using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class DepartmentUpdateAjaxViewModel
    {
        public DepartmentUpdateDto DepartmentUpdateDto { get; set; }
        //Eger validate doğru değilse partial döndürür.
        public string DepartmentUpdatePartial { get; set; }
        //Eklendikten sonra geri döner.
        public DepartmentDto DepartmentDto { get; set; }
    }
}
