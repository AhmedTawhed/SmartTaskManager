using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Helpers.CustomRequests;
using SmartTaskManager.Core.Helpers.CustomResults;

namespace SmartTaskManager.Core.Interfaces.IService
{
    public interface ITaskService
    {
        Task Add(TaskItem task);
        Task<bool> Delete(Guid taskId, string userId);
        Task<IEnumerable<TaskItem>> GetAll();
        Task<TaskItem> GetById(Guid id);
        Task<bool> Update(TaskItem task, string userId);
        Task<IEnumerable<TaskItem>> GetTasksForUser(string userId);
        Task<PagedList<TaskItem>> GetPage(GridRequest request,string userId);
        Task<bool> MarkTaskAsDone(Guid taskId, string userId);
    }
}