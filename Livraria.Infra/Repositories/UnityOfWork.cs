using Livraria.Infra.Context;
using Livraria.Shared.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace Livraria.Infra.Repositories
{
    public sealed class UnityOfWork : IUnityOfWork, IAsyncDisposable
    {
        private readonly AppDbContext _dbContext;
        private IDbContextTransaction? _dbContextTransaction;
        public UnityOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task SaveChangesAndCommitTransactionAsync()
        {
            await SaveChangesAsync();
            await CommitTransactionAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.Instance.SaveChangesAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_dbContextTransaction == null)
                throw new InvalidOperationException("Transação não iniciada, tem certeza que você chamou o método BeginTransaction()");
            await _dbContextTransaction.CommitAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _dbContextTransaction = await _dbContext.Instance.Database.BeginTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_dbContextTransaction == null)
                throw new InvalidOperationException("Transação não iniciada, tem certeza que você chamou o método BeginTransaction()");

            await _dbContextTransaction.RollbackAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_dbContextTransaction != null)
                await _dbContextTransaction.DisposeAsync();
        }
    }
}
