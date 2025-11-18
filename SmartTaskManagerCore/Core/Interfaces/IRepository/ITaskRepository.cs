using SmartTaskManagerCore.Core.Entities;

namespace SmartTaskManagerCore.Core.Interfaces.IRepository
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetUserTasksAsync(string userId);
        bool IsTaskRelatedToUser(Guid id, string userId);

    }
}
