using BookStore.Data.DbContext;
using BookStore.Domain.Common;
using BookStore.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Data.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IQueryable<TEntity> Query() => _dbSet;
        public TEntity Find(params object[] keyValues) => _dbSet.Find(keyValues);
        public async Task<TEntity> FindAsync(params object[] keyValues) => await _dbSet.FindAsync(keyValues);
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.FirstOrDefaultAsync(filter);
        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter, Expression<Func<TEntity, object>> include) =>
            await _dbSet.Include(include).FirstOrDefaultAsync(filter);

        public async Task<List<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task<List<TEntity>> GetAllFilteredAsync(Expression<Func<TEntity, bool>> filter) =>
            await _dbSet.Where(filter).ToListAsync();

        public virtual async Task<PagedList<TEntity>> GetPagedAsync<TKey>(
           Expression<Func<TEntity, bool>> filter,
           Expression<Func<TEntity, TKey>> orderByDescending,
           int page,
           int itemsPerPage)
        {
            var skip = (page - 1) * itemsPerPage;
            var query = _dbSet.AsQueryable();

            query = query.Where(filter);
            var total = await query.CountAsync();
            int pageCount = (int)Math.Ceiling(((double)total / itemsPerPage));
            var result = await query
                .OrderByDescending(orderByDescending)
                .Skip(skip)
                .Take(itemsPerPage)
                .ToListAsync();

            return new PagedList<TEntity>()
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                PageCount = pageCount,
                TotalItems = total,
                Items = result
            };
        }

        public async Task<PagedList<TEntity>> GetPagedAsync<TKey>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderByDescending,
            int page, int itemsPerPage,
            params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var skip = (page - 1) * itemsPerPage;
            var query = _dbSet.AsQueryable();

            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }

            query = query.Where(filter);
            var total = await query.CountAsync();
            int pageCount = (int)Math.Ceiling(((double)total / itemsPerPage));
            var result = await query
                .OrderByDescending(orderByDescending)
                .Skip(skip)
                .Take(itemsPerPage)
                .ToListAsync();

            return new PagedList<TEntity>()
            {
                Page = page,
                ItemsPerPage = itemsPerPage,
                PageCount = pageCount,
                TotalItems = total,
                Items = result
            };
        }

        public async Task<List<TEntity>> GetAllFilteredAsync<TKey>(
            Expression<Func<TEntity, bool>> filter,
            Expression<Func<TEntity, TKey>> orderByDescending)
            => await _dbSet.Where(filter).OrderByDescending(orderByDescending).ToListAsync();

        public async Task<List<TEntity>> GetAllFilteredIncludeAsync(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            var query = _dbSet.AsQueryable();
            if (includeExpressions != null)
            {
                foreach (var includeExpression in includeExpressions)
                {
                    query = query.Include(includeExpression);
                }
            }
            query = query.Where(filter);
            var result = await query.ToListAsync();
            return result;
        }

        public async Task DeleteAsync(params object[] keyValues)
        {
            var entity = await FindAsync(keyValues);
            if (entity == null) throw new BookStoreXException(404, "Item Not Found");
            await DeleteAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IList<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter) => await _dbSet.CountAsync(filter);
        public async Task<bool> IsExistAsync(Expression<Func<TEntity, bool>> filter) => await CountAsync(filter) > 0;

        public void AddRange(IList<TEntity> entities) => _dbSet.AddRange(entities);
        public void RemoveRange(IList<TEntity> entities) => _dbSet.RemoveRange(entities);

    }
}
