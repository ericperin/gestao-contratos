using ContractManager.Data.Interfaces;
using ContractManager.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContractManager.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntityBase
    {
        private readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) => _context = context;

        public virtual async Task Create(TEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;

            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var entityToDelete = _context.Set<TEntity>().SingleOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _context.Set<TEntity>().Remove(entityToDelete);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Edit(TEntity entity)
        {
            var editedEntity = _context.Set<TEntity>().SingleOrDefault(e => e.Id == entity.Id);
            editedEntity = entity;

            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TEntity>> FilterAsync()
        {
            return await _context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate)
        {
            return _context.Set<TEntity>().Where(predicate);
        }
    }
}
