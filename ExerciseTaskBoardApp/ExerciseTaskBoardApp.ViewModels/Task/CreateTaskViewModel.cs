using ExerciseTaskBoardApp.Data;
using System.ComponentModel.DataAnnotations;

namespace ExerciseTaskBoardApp.ViewModels.Task
{
    public class CreateTaskViewModel
    {
        [Required]
        [StringLength(DataConstants.MaxTaskTitle, MinimumLength = DataConstants.MinTaskTitle,
            ErrorMessage ="Title should be at least {2} characters long.")]
        public string? Title { get; set; }

        [Required]
        [StringLength(DataConstants.MaxTaskDescription, MinimumLength = DataConstants.MinTaskDesrtiption,
            ErrorMessage = "Description should be at least {2} characters long.")]
        public string? Description { get; set; }

        [Display(Name = "Board")]
        public int BoardId { get; set; }

        public IEnumerable<TaskBoardModel>? Boards { get; set; }
    }
}
