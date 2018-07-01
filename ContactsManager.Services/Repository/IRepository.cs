#region Namespaces

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        Task Insert(TEntity entity);

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="entity">TEntity</param>
        Task Update(TEntity entity);

        /// <summary>
        /// IsAnyExists
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        Task<bool> IsAnyExists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns>TEntity</returns>
        Task<IEnumerable<TEntity>> GetAll();

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns></returns>
        Task<TEntity> Get(Guid id);
    }
}
