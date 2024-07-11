using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs
{
    public class VisitorDto : DtoGetBase
    {
        public Visitor Visitor { get; set; }

    }
}
