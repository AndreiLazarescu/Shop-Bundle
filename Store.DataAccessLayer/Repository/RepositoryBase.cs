using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Store.Interfaces.Entity;
using Store.Interfaces.Repository;

namespace Store.DataAccessLayer.Repository
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected readonly IStoreContext Context;

        protected Expression<Func<TEntity, object>>[] BaseIncludes { get; set; }

        protected RepositoryBase(IStoreContext context)
        {
            Context = context;
        }

        public virtual IEnumerable<TEntity> GetEntities(int start, int end, Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IEnumerable<TEntity> entities = null;
            var dbSet = GetTrackingSet<TEntity>();

            dbSet = ApplyIncludes(dbSet, includes);

            if (where != null)
            {
                dbSet = dbSet.Where(where);
            }

            entities = dbSet.Skip(start).Take(end);

            return entities;
        }

        public virtual IEnumerable<TEntity> GetEntities(Expression<Func<TEntity, bool>> where = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IEnumerable<TEntity> entities = null;
            var dbSet = GetTrackingSet<TEntity>();

            dbSet = ApplyIncludes(dbSet, includes);

            if (where != null)
            {
                entities = dbSet.Where(where);
            }
            else
            {
                entities = dbSet;
            }

            return entities;
        }

        public virtual Task<TEntity> GetEntity(Expression<Func<TEntity, bool>> where, params Expression<Func<TEntity, object>>[] includes)
        {
            var dbSet = GetTrackingSet<TEntity>();

            dbSet = ApplyIncludes(dbSet, includes);

            return dbSet.FirstOrDefaultAsync(where);
        }

        public Task Delete(Expression<Func<TEntity, bool>> where)
        {
            var dbSet = GetTrackingSet<TEntity>();
            IEnumerable<TEntity> entities = dbSet.Where(where);

            foreach (var entity in entities)
            {
                Context.Set<TEntity>().Remove(entity);
            }

            return Save();
        }

        public Task Delete(TEntity entity)
        {
            Context.Set<TEntity>().Remove(ProcessEntity(entity));

            return Save();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public Task Insert(TEntity entity)
        {
            Context.Set<TEntity>().Add(ProcessEntity(entity));

            return Save();
        }

        public Task Save()
        {
            return Context.SaveChangesAsync();
        }

        public Task Update(TEntity entiy)
        {
            Context.Set<TEntity>().Update(entiy);

            return Save();
        }

        public Task<int> Count()
        {
            var dbset = GetNoTrackingSet<TEntity>();

            return dbset.CountAsync();
        }

        protected IQueryable<T> GetTrackingSet<T>() where T: class, IEntity
            => Context.Set<T>().AsQueryable();

        protected IQueryable<T> GetNoTrackingSet<T>() where T : class, IEntity
            => Context.Set<T>().AsNoTracking();

        protected IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> queryable, params Expression<Func<TEntity, object>>[] includes)
        {
            if (BaseIncludes != null)
            {
                foreach (var baseInclude in BaseIncludes)
                {
                    queryable = queryable.Include(baseInclude);
                }
            }

            if (!includes.Any()) return queryable;

            foreach (var include in includes)
            {
                queryable = queryable.Include(include);
            }


            return queryable;
        }

        protected virtual TEntity ProcessEntity(TEntity entity)
        {
            return entity;
        }
    }
}
