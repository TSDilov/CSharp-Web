using ExerciseTaskBoardApp.Models;
using ExerciseTaskBoardApp.Service;
using ExerciseTaskBoardApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace ExerciseTaskBoardApp.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IBoardService boardService;
        private readonly ITaskService taskService;

        public HomeController(
            IBoardService boardService,
            ITaskService taskService)
        {
            this.boardService = boardService;
            this.taskService = taskService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var tasksCounts = this.boardService.GetHomeModel();
            var userTasksCount = -1;
            if (this.User.Identity.IsAuthenticated)
            {
                var currentUserId = this.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                userTasksCount = this.taskService.GetUsersTasksCount(currentUserId);
            }

            var homeModel = new HomeViewModel()
            {
                AllTasksCount = this.taskService.GetAllTasksCount(),
                BoardsWithTasksCount = tasksCounts,
                UserTasksCount = userTasksCount,
            };

            return View(homeModel);
        }
    }
}