using ExerciseTaskBoardApp.ViewModels;
using ExerciseTaskBoardApp.ViewModels.Task;

namespace ExerciseTaskBoardApp.Service
{
    public interface IBoardService
    {
        IEnumerable<BoardViewModel> GetAll();

        IEnumerable<TaskBoardModel> GetBoards();

        List<HomeBoardModel> GetHomeModel();
    }
}
