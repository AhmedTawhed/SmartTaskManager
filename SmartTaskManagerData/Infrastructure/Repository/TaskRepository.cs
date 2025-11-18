using Microsoft.EntityFrameworkCore;
using SmartTaskManagerCore.Core.Entities;
using SmartTaskManagerCore.Core.Interfaces.IRepository;
using SmartTaskManagerData.Infrastructure.Data;
using SmartTaskManagerData.Infrastructure.Repositories;

namespace SmartTaskManagerData.Infrastructure.Repository
{
    public class TaskRepository : Repository<TaskItem>, ITaskRepository
    {
        public TaskRepository(TaskDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskItem>> GetUserTasksAsync(string userId)
        {
            return await _context.TaskItems.Where(t => t.UserId == userId).
                OrderByDescending(t => t.CreatedAt).ToListAsync();
        }
        public bool IsTaskRelatedToUser(Guid id, string userId)
        {
            return _context.TaskItems.Any(t => t.Id == id && t.UserId == userId);
        }
    }
}