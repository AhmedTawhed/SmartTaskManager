using Microsoft.EntityFrameworkCore;
using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Interfaces.IRepository;
using SmartTaskManager.Infrastructure.Data;

namespace SmartTaskManager.Infrastructure.Repository
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(TaskDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<TaskItem>> GetUserTasksAsync(string userId)
        {
            return await _context.TaskItems
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }
        public bool IsTaskRelatedToUser(Guid id, string userId)
        {
            return _context.TaskItems.Any(t => t.Id == id && t.UserId == userId);
        }
        public async Task<TaskItem?> GetTaskByIdForUser(Guid id, string userId)
        {
            return await _context.TaskItems
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }
    }
}