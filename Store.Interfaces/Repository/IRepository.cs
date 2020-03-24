using Store.Interfaces.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable
        where TEntity : IEntity
    {
        IEnumerable<TEntity> GetEntities(int start, int end, Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes);

        Task Insert(TEntity entity);

        Task Delete(Expression<Func<TEntity, bool>> where);
        Task Delete(TEntity entity);

        Task Update(TEntity entiy);
        Task Save();

        Task<int> Count();
    }
}
