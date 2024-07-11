using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.UI.Models
{
    public class UserWithRolesViewModel
    {
        public User User { get; set; }
        public IList<string> Roles { get; set; }
    }
}
