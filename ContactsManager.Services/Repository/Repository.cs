#region Namespaces

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace ContactsManager.Repository.Repository
{
    /// <summary>
    /// Generic Repository Implementation
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        #region Variables and Constructor

        /// <summary>
        /// DbContext
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// Repository - Constructor
        /// </summary>
        /// <param name="dbContext"></param>
        public Repository(DbContext dbContext)
        {
            Context = dbContext;
        }

        #endregion

        /// <summary>
        /// Insert Record
        /// </summary>
        /// <param name="entity">TEntity</param>
        public virtual void Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Update Record
        /// </summary>
        /// <param name="entity">TEntity</param>
        public void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// IsAnyExists
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        public bool IsAnyExists(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Any(predicate);
        }

        /// <summary>
        /// GetAll Records
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>TEntity</returns>
        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// GetBy Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>TEntity</returns>
        public TEntity Get(Guid id)
        {
            return Context.Set<TEntity>().Find(id);
        }
    }
}
