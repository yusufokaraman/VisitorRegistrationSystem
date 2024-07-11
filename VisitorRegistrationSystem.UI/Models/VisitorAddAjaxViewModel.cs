using VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs;

namespace VisitorRegistrationSystem.UI.Models
{
    public class VisitorAddAjaxViewModel
    {
        public VisitorAddDto VisitorAddDto { get; set; }
        public string VisitorAddPartial { get; set; }
        public VisitorDto VisitorDto { get; set; }

    }

}
