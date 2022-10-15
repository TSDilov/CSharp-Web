using ExerciseTaskBoardApp.Data;
using ExerciseTaskBoardApp.ViewModels;
using ExerciseTaskBoardApp.ViewModels.Task;

namespace ExerciseTaskBoardApp.Service
{
    public class BoardService : IBoardService
    {
        private readonly ApplicationDbContext dbContext;

        public BoardService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<BoardViewModel> GetAll()
        {
            var boards = this.dbContext.Boards
                .Select(b => new BoardViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                    Tasks = b.Tasks.Select(t => new ViewModels.Task.TaskViewModel 
                    { 
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        Owner = t.Owner.UserName,
                    }),
                })
                .ToList();

            return boards;
        }

        public IEnumerable<TaskBoardModel> GetBoards()
        {
            return this.dbContext.Boards
                .Select(b => new TaskBoardModel
                {
                    Id = b.Id,
                    Name = b.Name,
                })
                .ToList();
        }
    }
}
