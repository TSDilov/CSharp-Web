using ExerciseTaskBoardApp.ViewModels.Task;

namespace ExerciseTaskBoardApp.Service
{
    public interface ITaskService
    {
        Task CreateAsync(CreateTaskViewModel model, string userId);
    }
}
