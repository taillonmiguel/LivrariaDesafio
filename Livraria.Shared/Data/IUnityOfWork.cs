namespace Livraria.Shared.Data
{
    public interface IUnityOfWork
    {
        Task SaveChangesAndCommitTransactionAsync();
        Task SaveChangesAsync();
        Task CommitTransactionAsync();
        Task BeginTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
