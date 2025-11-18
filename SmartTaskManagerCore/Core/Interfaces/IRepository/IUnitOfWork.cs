namespace SmartTaskManagerCore.Core.Interfaces.IRepository
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ITaskRepository Tasks { get; }
        Task<int> CompleteAsync();

    }
}
