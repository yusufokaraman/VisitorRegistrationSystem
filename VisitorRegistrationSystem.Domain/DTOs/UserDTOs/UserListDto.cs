using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.UserDTOs
{
    public class UserListDto : DtoGetBase
    {
        public IList<User> Users { get; set; }
    }
}
