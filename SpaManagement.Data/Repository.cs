using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using SpaManagement.Data.Abstract;

namespace SpaManagement.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        SpaManagementContext _spaManagementContext; //= database
        public Repository(SpaManagementContext spaManagementContext)
        {
            _spaManagementContext = spaManagementContext;
        }

        public async Task<IEnumerable<T>> GetData(Expression<Func<T, bool>> expression = null)
        {
            if (expression == null)
            {
                return await _spaManagementContext.Set<T>().ToListAsync();
            }

            return await _spaManagementContext.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<T?> GetById(object id)
        {
            return await _spaManagementContext.Set<T>().FindAsync(id); //return { name: '', description: ''},
        }

        public async Task<T?> GetSingleByConditionAsync(Expression<Func<T, bool>> expression = null)
        {
            return await _spaManagementContext.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(T entity)
        {
            await _spaManagementContext.Set<T>().AddAsync(entity);
        }

        public async Task Insert(IEnumerable<T> entities)
        {
            await _spaManagementContext.Set<T>().AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            EntityEntry entityEntry = _spaManagementContext.Entry<T>(entity);
            entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        public void Delete(T entity)
        {
            EntityEntry entityEntry = _spaManagementContext.Entry<T>(entity);
            entityEntry.State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }

        public void Delete(Expression<Func<T, bool>> expression)
        {
            var entities = _spaManagementContext.Set<T>().Where(expression).ToList();
            if (entities.Count > 0)
            {
                _spaManagementContext.Set<T>().RemoveRange(entities);
            }
        }

        public virtual IQueryable<T> Table => _spaManagementContext.Set<T>();

        public async Task Commit()
        {
            await _spaManagementContext.SaveChangesAsync();
        }


    }
}

