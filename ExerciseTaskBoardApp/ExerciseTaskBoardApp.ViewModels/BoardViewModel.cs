using ExerciseTaskBoardApp.ViewModels.Task;

namespace ExerciseTaskBoardApp.ViewModels
{
    public class BoardViewModel
    {
        public BoardViewModel()
        {
            this.Tasks = new List<TaskViewModel>();
        }

        public int Id { get; set; }

        public string? Name { get; set; }

        public IEnumerable<TaskViewModel> Tasks { get; set; }
    }
}
