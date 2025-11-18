using SmartTaskManagerCore.Core.Entities;
using SmartTaskManagerCore.Core.Interfaces.IRepository;
using SmartTaskManagerCore.Core.Interfaces.IService;

namespace SmartTaskManager.Application.Services
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
    }
}