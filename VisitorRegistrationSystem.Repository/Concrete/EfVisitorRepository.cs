using Microsoft.EntityFrameworkCore;
using VisitorRegistrationSystem.Common.Repository.Concrete;
using VisitorRegistrationSystem.Domain.Entity;
using VisitorRegistrationSystem.Repository.IRepository;

namespace VisitorRegistrationSystem.Repository.Concrete
{
    public class EfVisitorRepository : EfEntityRepositoryBase<Visitor>, IVisitorRepository
    {
        public EfVisitorRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
