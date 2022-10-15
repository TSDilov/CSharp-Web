using ExerciseTaskBoardApp.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseTaskBoardApp.Controllers
{
    public class BoardController : BaseController
    {
        private readonly IBoardService boardService;

        public BoardController(IBoardService boardService)
        {
            this.boardService = boardService;
        }

        public IActionResult All()
        {
            var boards = this.boardService.GetAll();
            return View(boards);
        }
    }
}
