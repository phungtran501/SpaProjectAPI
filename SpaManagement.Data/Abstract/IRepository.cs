using System.Linq.Expressions;

namespace SpaManagement.Data.Abstract
{
    public interface IRepository<T> where T : class
    {
        Task Commit();
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> expression);
        Task<T?> GetById(object id);
        Task<IEnumerable<T>> GetData(Expression<Func<T, bool>> expression = null);
        Task<T?> GetSingleByConditionAsync(Expression<Func<T, bool>> expression = null);
        Task Insert(T entity);
        Task Insert(IEnumerable<T> entities);
        void Update(T entity);
    }
}