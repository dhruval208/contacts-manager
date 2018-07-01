#region Namespaces

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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
        public virtual async Task Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Update Record
        /// </summary>
        /// <param name="entity">TEntity</param>
        public async Task Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// IsAnyExists
        /// </summary>
        /// <param name="predicate">Predicate</param>
        /// <returns>bool</returns>
        public async Task<bool> IsAnyExists(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().AnyAsync(predicate);
        }

        /// <summary>
        /// GetAll Records
        /// </summary>
        /// <returns>TEntity</returns>
        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        /// <summary>
        /// GetBy Id
        /// </summary>
        /// <param name="id">Guid</param>
        /// <returns>TEntity</returns>
        public async Task<TEntity> Get(Guid id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
    }
}
