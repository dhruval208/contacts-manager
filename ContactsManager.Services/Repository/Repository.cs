using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace ContactsManager.Repository.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext dbContext)
        {
            Context = dbContext;
        }

        public void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public bool IsAnyExists(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public TEntity Get(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }
    }
}
