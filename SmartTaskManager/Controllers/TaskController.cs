using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartTaskManager.Core.Entities;
using SmartTaskManager.Core.Helpers.CustomRequests;
using SmartTaskManager.Core.Interfaces.IService;

namespace SmartTaskManager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly UserManager<ApplicationUser> _userManager;

        public TaskController(ITaskService taskService, UserManager<ApplicationUser> userManager)
        {
            _taskService = taskService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string sortColumn, string sortDir, string search, int page = 1)
        {
            string userId = _userManager.GetUserId(User);

            var gridRequest = new GridRequest
            {
                PageNumber = page,
                SortColumn = sortColumn,
                SortDirection = sortDir,
                SearchText = search
            };

            var tasks = await _taskService.GetPage(gridRequest,userId);

            ViewData["CurrentSortColumn"] = sortColumn ?? "CreatedAt";
            ViewData["CurrentSortDir"] = sortDir ?? "asc";
            ViewData["CurrentSearch"] = search ?? "";
            ViewData["Page"] = page;
            ViewData["TotalPages"] = tasks.NumberOfPages;

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_TasksTablePartial", tasks.Items);

            return View(tasks.Items);
        }
        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (!ModelState.IsValid)
                return View(task);
            task.UserId = _userManager.GetUserId(User);
            await _taskService.Add(task);
            TempData["SuccessMessage"] = "Task created successfully!";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var task = await _taskService.GetById(id);
            var userId = _userManager.GetUserId(User);
            if (task == null || task.UserId != userId) return NotFound();
            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (!ModelState.IsValid)
                return View(task);

            string userId = _userManager.GetUserId(User);

            bool updated = await _taskService.Update(task, userId);
            if (!updated)
                return NotFound();

            TempData["SuccessMessage"] = "Task updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = _userManager.GetUserId(User);

            var deleted = await _taskService.Delete(id, userId);

            if (!deleted)
                return Unauthorized();

            TempData["SuccessMessage"] = "Task deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsDone(Guid id)
        {
            var userId = _userManager.GetUserId(User);
            var marked = await _taskService.MarkTaskAsDone(id, userId);
            if (!marked)
                return NotFound();

            TempData["SuccessMessage"] = "Task marked as done!";
            return RedirectToAction(nameof(Index));
        }
    }
}