using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RepositoryLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.IRepositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> Paginated(int offset, int limit);
        Task<T?> GetByIdAsync(int id);
        Task InsertAsync(T entity);
        Task InsertRange(List<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
        Task <T?> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T?> FirstAsync();
        Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> SortedFilteredPaginated<TOrder>(Expression<Func<T, bool>> predicate, Expression<Func<T,TOrder>> sort, bool isAsc, int offset, int limit);
        Task<int>Count(Expression<Func<T, bool>> predicate);
        Task<int> Count();
        ApplicationDbContext GetContext();
    }
}
