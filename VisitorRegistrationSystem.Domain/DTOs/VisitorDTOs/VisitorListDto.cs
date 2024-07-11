using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs
{
    public class VisitorListDto : DtoGetBase
    {
        public IList<Visitor> Visitors { get; set; }
    }
}
