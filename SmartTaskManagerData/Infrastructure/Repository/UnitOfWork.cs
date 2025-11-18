using SmartTaskManagerCore.Core.Interfaces.IRepository;
using SmartTaskManagerData.Infrastructure.Data;

namespace SmartTaskManagerData.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;
        public ITaskRepository Tasks { get; private set; }

        public UnitOfWork(TaskDbContext context)
        {
            _context = context;
            Tasks = new TaskRepository(_context);
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}