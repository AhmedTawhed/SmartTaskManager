using SmartTaskManagerCore.Core.Entities;

namespace SmartTaskManagerCore.Core.Interfaces.IService
{
    public interface ITaskService
    {
        Task Add(TaskItem task);
        Task<bool> Delete(Guid taskId, string userId);
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem> GetById(Guid id);
        Task<bool> Update(TaskItem task, string userId);
        Task<IEnumerable<TaskItem>> GetTasksForUser(string userId);
    }
}