﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Common.Repository.IRepository;

namespace VisitorRegistrationSystem.Common.Repository.Concrete
{
    public class EfEntityRepositoryBase<Tentity> : IEntityRepository<Tentity>
             where Tentity : class, IEntity, new()
    {
        private readonly DbContext _dbContext;
        public EfEntityRepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tentity> AddAsync(Tentity entity)
        {
            await _dbContext.Set<Tentity>().AddAsync(entity);
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<Tentity, bool>> predicate)
        {
            return await _dbContext.Set<Tentity>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<Tentity, bool>> predicate)
        {
            return await _dbContext.Set<Tentity>().CountAsync(predicate);
        }

        public async Task DeleteAsync(Tentity entity)
        {
            await Task.Run(() => { _dbContext.Set<Tentity>().Remove(entity); });
        }

        public async Task<IList<Tentity>> GetAllAsync(Expression<Func<Tentity, bool>> predicate, params Expression<Func<Tentity, object>>[] includeProperties)
        {
            IQueryable<Tentity> query = _dbContext.Set<Tentity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);

                }
            }
            return await query.ToListAsync();
        }

        public async Task<Tentity> GetAsync(Expression<Func<Tentity, bool>> predicate, params Expression<Func<Tentity, object>>[] includeProperties)
        {
            IQueryable<Tentity> query = _dbContext.Set<Tentity>();
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    query = query.Include(includeProperty);

                }
            }
            return await query.SingleOrDefaultAsync();
        }

        public async Task<Tentity> UpdateAsync(Tentity entity)
        {

            await Task.Run(() => { _dbContext.Set<Tentity>().Update(entity); });

            return entity;
        }
    }
}
