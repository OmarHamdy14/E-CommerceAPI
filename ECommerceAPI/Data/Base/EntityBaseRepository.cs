using ECommerceAPI.Models;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ECommerceAPI.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<T> _dbset;
        public EntityBaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbset = _appDbContext.Set<T>();
        }
        public async Task<T> Get(Expression<Func<T, bool>>? filter, string? IncludeProperties = null, bool IsTracked = false)
        {
            IQueryable<T> query;
            if (!IsTracked) query = _dbset.AsNoTracking();
            else query = _dbset;

            query = query.Where(filter);

            foreach (var property in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(property);
            }

            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<T>> GetAll(Expression<Func<T, bool>>? filter, string? IncludeProperties = null)
        {
            IQueryable<T> query = _dbset;
            query = query.Where(filter);

            foreach (var property in IncludeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query.Include(property);
            }

            return await query.ToListAsync();
        }
        public async Task Create(T entity)
        {
            _dbset.Add(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Update(T entity)
        {
            EntityEntry entry = _appDbContext.Entry(entity);
            entry.State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
        public async Task Remove(T entity)
        {
            _dbset.Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task RemoveRange(List<T> entities)
        {
            _dbset.RemoveRange(entities);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
