using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Api.DAL.Repository
{
    public class AppDbContextRepository<T> : IRepository<T> where T : class, IAppDbContextEntity
    {
        internal AppDbContext dbContext;
        internal DbSet<T> dbSet;
        public AppDbContextRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }
        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(includeProperty);

            if (orderBy != null)
                return orderBy(query).ToList();

            return query.ToList();
        }

        public T Get(object id)
        {
            return dbSet.Find(id);
        }

        public bool Insert (T entity)
        {
            return dbSet.Add(entity).State == EntityState.Added;
        }

        public bool Update (T entity)
        {
            dbSet.Attach(entity);
            var state = dbContext.Entry(entity).State = EntityState.Modified;
            return state == EntityState.Modified;
        }

        public bool Delete (T entity)
        {
            entity.IsDeleted = true;
            return this.Update(entity);
        }

        public bool Any()
        {
            return dbSet.Any();
        }
    }
}