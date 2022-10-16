using ExerciseTaskBoardApp.Data.Models;
using ExerciseTaskBoardApp.ViewModels.Task;

namespace ExerciseTaskBoardApp.Service
{
    public interface ITaskService
    {
        Task CreateAsync(CreateTaskViewModel model, string userId);

        Task<TaskDetailsViewModel> GetTaskByIdAsync(int id);

        TaskA GetById(int id);

        Task EditAsync(CreateTaskViewModel model, int id);

        Task DeleteAsync(int id);
    }
}
