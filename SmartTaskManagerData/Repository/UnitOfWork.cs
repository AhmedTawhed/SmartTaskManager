using SmartTaskManager.Core.Interfaces.IRepository;
using SmartTaskManager.Infrastructure.Data;

namespace SmartTaskManager.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;
        ITaskRepository _tasks;

        public UnitOfWork(TaskDbContext context)
        {
            _context = context;
        }

        public ITaskRepository Tasks 
        { 
            get 
            {
                if (_tasks == null)
                {
                    _tasks = new TaskRepository(_context);
                }
                return _tasks;
            }
        }
        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await _context.DisposeAsync();
    }
}