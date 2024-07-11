using Microsoft.EntityFrameworkCore;
using VisitorRegistrationSystem.Common.Repository.Concrete;
using VisitorRegistrationSystem.Domain.Entitiy;
using VisitorRegistrationSystem.Repository.IRepository;

namespace VisitorRegistrationSystem.Repository.Concrete
{
    public class EfDepartmentRepository : EfEntityRepositoryBase<Department>, IDepartmentRepository
    {
        public EfDepartmentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
