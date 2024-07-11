using Microsoft.AspNetCore.Identity;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.Entitiy
{
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
        public ICollection<Visitor> Visitors { get; set; }
    }
}
