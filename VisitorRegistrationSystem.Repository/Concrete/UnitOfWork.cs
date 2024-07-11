using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationSystem.Repository.Context;
using VisitorRegistrationSystem.Repository.IRepository;

namespace VisitorRegistrationSystem.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly VisitorDbContext _context;

        private EfVisitorRepository _visitorRepository;

        private EfDepartmentRepository _departmentRepository;

        public UnitOfWork(VisitorDbContext context)
        {
            _context = context;

        }


        public IVisitorRepository Visitors => _visitorRepository ?? new EfVisitorRepository(_context);



        public IDepartmentRepository Departments => _departmentRepository ?? new EfDepartmentRepository(_context);



        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }


    }
}
