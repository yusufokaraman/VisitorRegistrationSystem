using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.Entitiy
{
    public class Department : EntityBase, IEntity
    {
        public string Name { get; set; }
        public long CountactNo { get; set; }
        public ICollection<Visitor> Visitors { get; set; }

    }
}
