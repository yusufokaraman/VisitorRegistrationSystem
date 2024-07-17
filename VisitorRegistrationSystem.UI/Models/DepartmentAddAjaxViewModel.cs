using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class DepartmentAddAjaxViewModel
    {
        public DepartmentAddDto DepartmentAddDto { get; set; }
        public string DepartmentAddPartial { get; set; }

        public DepartmentDto DepartmentDto { get; set; }
    }
}
