using ContractManager.Domain.Entities;
using System;
using System.Collections.Generic;

namespace ContractManager.Data.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : IEntityBase
    {
        void Create(TEntity entity);
        void Delete(TEntity entity);
        void Delete(Guid id);
        void Edit(TEntity entity);

        TEntity GetById(Guid id);
        IEnumerable<TEntity> Filter();
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);

        void SaveChanges();
    }
}
