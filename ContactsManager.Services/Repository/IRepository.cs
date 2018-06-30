using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactsManager.Repository.Repository
{
    public interface IRepository<TEntity>
    {
        void Insert(TEntity entity);
        void Update(TEntity entity);
        bool IsAnyExists(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Guid id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    }
}
