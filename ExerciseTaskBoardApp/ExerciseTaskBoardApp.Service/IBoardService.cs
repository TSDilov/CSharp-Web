using ExerciseTaskBoardApp.ViewModels;

namespace ExerciseTaskBoardApp.Service
{
    public interface IBoardService
    {
        IEnumerable<BoardViewModel> GetAll();
    }
}
