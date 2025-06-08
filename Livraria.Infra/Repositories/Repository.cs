using Livraria.Domain.Repositories;
using Livraria.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Livraria.Infra.Repositories
{
    public class Repository<TEntity>(DbContext _context) : IRepository<TEntity> where TEntity : class, IHaveId
    {
        protected readonly DbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        public async Task<TEntity> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();

        public async Task AddAsync(TEntity entity) => await _dbSet.AddAsync(entity);

        public void Update(TEntity entity) => _dbSet.Update(entity);

        public void Remove(TEntity entity) => _dbSet.Remove(entity);

        public async Task SaveChangesAsync() => await _context.SaveChangesAsync();

        protected internal IQueryable<TEntity> GetQuery()
        {
            return _dbSet.AsQueryable();
        }

        public virtual async Task<bool> ExistsAsync(Guid id)
        {
            var query = GetQuery();

            return await query.AnyAsync(x => x.Id == id);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, object>> property, object value)
        {
            var query = GetQuery();

            return await query.Where($"{property.GetPropertyAccess().Name} == @0", value).AnyAsync();
        }
    }
}