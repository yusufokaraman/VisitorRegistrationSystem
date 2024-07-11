using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.Entity
{
    public class Visitor : EntityBase, IEntity
    {
        public string TcNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ContactNo { get; set; }
        public DateTime BirthDate { get; set; }

        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }


        public bool IsExit { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime OutDate { get; set; }
    }
}
