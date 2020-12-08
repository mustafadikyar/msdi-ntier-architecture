using Msdi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Msdi.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> GetAll(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includeProperties);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);
    }
}
