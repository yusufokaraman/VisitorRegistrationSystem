using VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class DepartmentUpdateAjaxViewModel
    {
        public DepartmentUpdateDto DepartmentUpdateDto { get; set; }
        public string DepartmentUpdatePartial { get; set; }

        public DepartmentDto DepartmentDto { get; set; }
    }
}
