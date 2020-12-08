using Microsoft.EntityFrameworkCore;
using Msdi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Msdi.Core.DataAccess.EntityFramework
{
    public class EFEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
      where TEntity : class, IEntity, new()
      where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                var createdEntity = context.Entry(entity);
                createdEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var removed = context.Entry(entity);
                removed.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }


        public virtual IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            TContext queryContext = new TContext();

            IQueryable<TEntity> query;
            query = filter != null ? queryContext.Set<TEntity>().Where(filter) : queryContext.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query;
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updated = context.Entry(entity);
                updated.State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        //public TEntity Get(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includeProperties)
        //{
        //    using (var context = new TContext())
        //    {
        //        IQueryable<TEntity> query = context.Set<TEntity>();
        //        foreach (var includeProperty in includeProperties)
        //        {
        //            query = query.Include(includeProperty);
        //        }

        //        return query.Where(predicate).FirstOrDefault();
        //    }
        //}
    }
}