using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Helpers.CustomRequests;
using SmartTaskManager.Core.Helpers.CustomResults;
using SmartTaskManager.Core.Helpers.Enums;
using SmartTaskManager.Core.Interfaces.IRepository;
using SmartTaskManager.Core.Interfaces.IService;

namespace SmartTaskManager.Infrastructure.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<TaskItem>> GetAll() => await _unitOfWork.Tasks.GetAll();
        public async Task<IEnumerable<TaskItem>> GetTasksForUser(string userId) => await _unitOfWork.Tasks.GetUserTasksAsync(userId);
        public async Task<TaskItem> GetById(Guid id) => await _unitOfWork.Tasks.GetById(id);
        public async Task Add(TaskItem task)
        {
            if (string.IsNullOrEmpty(task.UserId))
                throw new ArgumentException("UserId must be set for a task.");
            await _unitOfWork.Tasks.Add(task);
            await _unitOfWork.CompleteAsync();
        }
        public async Task<bool> Update(TaskItem task, string userId)
        {
            var isTaskRelated = _unitOfWork.Tasks.IsTaskRelatedToUser(task.Id, userId);
            if (!isTaskRelated)
                return false;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<bool> Delete(Guid taskId, string userId)
        {
            var isTaskRelated = _unitOfWork.Tasks.IsTaskRelatedToUser(taskId, userId);
            if (!isTaskRelated)
                return false;
            
            _unitOfWork.Tasks.Delete(taskId);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<PagedList<TaskItem>> GetPage(GridRequest request,string userId)
        {
            return await _unitOfWork.Tasks.GetPage(request, t => t.UserId == userId);
        }
        public async Task<bool> MarkTaskAsDone(Guid taskId, string userId)
        {
            var task = await _unitOfWork.Tasks.GetTaskByIdForUser(taskId, userId);
            if (task == null)
                return false;

            task.Status = StatusEnum.Done;

            _unitOfWork.Tasks.Update(task);
            await _unitOfWork.CompleteAsync();
            return true;
        }

    }
}