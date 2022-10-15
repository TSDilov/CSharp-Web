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
            return RedirectToAction("All", "Boards");
        }
    }
}
