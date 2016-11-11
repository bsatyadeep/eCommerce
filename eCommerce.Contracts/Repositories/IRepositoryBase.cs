using System.Linq;
namespace eCommerce.Contracts.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        T GetById(object id);
        IQueryable<T> GetAll();
        IQueryable<T> GetPaged(int top = 20, int skip = 0, object orderBy = null, object filter = null);
        IQueryable<T> GetAll(object filter);
        T GetFullObject(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(object id);
        void Commit();
        void Dispose();
    }
}
