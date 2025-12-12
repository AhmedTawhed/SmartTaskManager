using SmartTaskManager.Core.Entities;

namespace SmartTaskManager.Core.Interfaces.IRepository
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetUserTasksAsync(string userId);
        bool IsTaskRelatedToUser(Guid id, string userId);
        Task<TaskItem?> GetTaskByIdForUser(Guid id, string userId);

    }
}
