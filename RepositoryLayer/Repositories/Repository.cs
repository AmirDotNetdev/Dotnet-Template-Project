using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Data;
using RepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _entity;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _entity = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entity.Where(a => !a.isDelete).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _entity.FindAsync(id);
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await _entity.AddAsync(entity);
        }

        public async Task InsertRange(List<T> entities)
        {
            await _entity.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _entity.Update(entity);
        }

        public void Remove(T entity)
        {
            entity.isDelete = true;
            entity.Deleted = DateTime.Now;
            _entity.Update(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            foreach (var entity in entities)
            {
                entity.isDelete = true;
                entity.Deleted = DateTime.Now;
                _entity.Update(entity);
            }
        }


        public async Task<int> Count(Expression<Func<T, bool>> predicate)
        {
            return await _entity.Where(predicate).Where(a => !a.isDelete).CountAsync();
        }

        public async Task<int> Count()
        {
            return await _entity.Where(a => !a.isDelete).CountAsync();
        }

        public async Task<IEnumerable<T>> Filter(Expression<Func<T, bool>> predicate)
        {
            return await _entity.Where(predicate).Where(a => !a.isDelete).ToListAsync();
        }

        public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entity.Where(a=> !a.isDelete).FirstOrDefaultAsync(predicate);
        }

        public async Task<T?> FirstAsync()
        {
            return await _entity.FirstOrDefaultAsync();
        }

        

        

        public ApplicationDbContext GetContext()
        {
            return _dbContext;
        }

        

        

        public async Task<IEnumerable<T>> Paginated(int offset, int limit)
        {
            return await _entity.Where(a=> !a.isDelete).Skip(offset).Take(limit).ToListAsync();

        }

        

        

        public async Task<IEnumerable<T>> SortedFilteredPaginated<TOrder>(Expression<Func<T, bool>> predicate, Expression<Func<T, TOrder>> sort, bool isAsc, int offset, int limit)
        {
            if (isAsc)
            {
                return await _entity.Where(predicate).Where(a=> !a.isDelete).OrderBy(sort).Take(limit).Skip(offset).ToListAsync();
            }
            else
            {
                return await _entity.Where(predicate).Where(a => !a.isDelete).OrderByDescending(sort).Take(limit).Skip(offset).ToListAsync();
            }
        }

        
    }
}
