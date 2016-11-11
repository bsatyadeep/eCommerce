using eCommerce.DataAccess.Data;
using System.Data.Entity;
using System.Linq;
using eCommerce.Contracts.Repositories;

namespace eCommerce.DataAccess.Repositories
{

    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        internal DataContext dbContext;
        internal DbSet<T> dbSet;

        protected RepositoryBase(DataContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null)
        {
            return null;//need to override inorder to implement specific filters and ordering
        }

        public virtual IQueryable<T> GetAll(object filter)
        {
            return null;//need to override inorder to implement specific filters
        }

        public virtual T GetFullObject(object id)
        {
            return null;//need to override inorder to implement specific object
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            //if (dbContext.Entry(entity).State == EntityState.Detached)
            //{
            //    dbSet.Attach(entity);
            //    dbSet.Remove(entity);
            //}
            if (dbContext.Entry(entity).State == EntityState.Detached)
                dbSet.Attach(entity);

            dbSet.Remove(entity);
        }

        public virtual void Delete(object id)
        {
            T entity = dbSet.Find(id);
            Delete(entity);
        }

        public virtual void Commit()
        {
            dbContext.SaveChanges();
        }

        public virtual void Dispose()
        {
            dbContext.Dispose();
        }
    }
}