#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

#endregion

namespace ContactsManager.Repository.Repository
{
    /// <summary>
    /// IRepository - Generic Repository
    /// </summary>
    /// <typeparam name="TEntity">TEntity</typeparam>
    public interface IRepository<TEntity>
    {
        /// <summary>
        /// Insert Record
        /// </summary>
        /// <param name="entity">TEntity</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">TEntity</param>
        void Update(TEntity entity);

        /// <summary>
        /// IsAnyExists
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        bool IsAnyExists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        TEntity Get(Guid id);
    }
}
