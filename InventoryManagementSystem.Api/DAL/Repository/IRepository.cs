using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace InventoryManagementSystem.Api.DAL.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");
        T Get(object id);
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool Any();
    }
}