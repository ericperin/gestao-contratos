using ContractManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContractManager.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : IEntityBase
    {
        Task Create(TEntity entity);
        Task Delete(TEntity entity);
        Task Delete(Guid id);
        Task SoftDelete(Guid id);
        Task Edit(TEntity entity);

        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<TEntity>> FilterAsync();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
    }
}
