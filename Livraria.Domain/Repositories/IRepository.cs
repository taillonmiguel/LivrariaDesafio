using System.Linq.Expressions;

namespace Livraria.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
        Task SaveChangesAsync();
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, object>> property, object value);
    }
}
