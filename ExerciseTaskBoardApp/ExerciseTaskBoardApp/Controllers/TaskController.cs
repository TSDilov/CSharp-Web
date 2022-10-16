using ExerciseTaskBoardApp.Service;
using ExerciseTaskBoardApp.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace ExerciseTaskBoardApp.Controllers
{
    public class TaskController : BaseController
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public TaskController(
            IBoardService boardService,
            ITaskService taskService
            )
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        public IActionResult Create()
        {
            CreateTaskViewModel model = new CreateTaskViewModel()
            {
                Boards = this.boardService.GetBoards()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskViewModel model)
        {
            if (!this.boardService.GetBoards().Any(b => b.Id == model.BoardId))
            {
                this.ModelState.AddModelError(nameof(model.BoardId), "Board does not exist!");
            }

            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.taskService.CreateAsync(model, currentUserId);
            return RedirectToAction("All", "Board");
        }

        public async Task<IActionResult> Details(int id)
        {
            var task = await this.taskService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return BadRequest();
            }

            return View(task);
        }

        public IActionResult Edit(int id)
        {
            var task = this.taskService.GetById(id);
            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            CreateTaskViewModel model = new CreateTaskViewModel()
            {
                Title = task.Title,
                Description = task.Description,
                BoardId = task.BoardId,
                Boards = this.boardService.GetBoards(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreateTaskViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            await this.taskService.EditAsync(model, id);
            return RedirectToAction("All", "Board");
        }

        public IActionResult Delete(int id)
        {
            var task = this.taskService.GetById(id);
            if (task == null)
            {
                return BadRequest();
            }

            var currentUserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (currentUserId != task.OwnerId)
            {
                return Unauthorized();
            }

            TaskViewModel model = new TaskViewModel()
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TaskViewModel model)
        {
            await this.taskService.DeleteAsync(model.Id);
            return RedirectToAction("All", "Board");
        }
    }
}
