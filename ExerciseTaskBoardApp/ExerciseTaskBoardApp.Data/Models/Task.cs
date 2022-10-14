using System.ComponentModel.DataAnnotations;

namespace ExerciseTaskBoardApp.Data.Models
{
    public class TaskA
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxTaskTitle)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxTaskDescription)]
        public string? Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public int BoardId { get; set; }

        public Board? Board { get; set; }

        [Required]
        public string? OwnerId { get; set; }

        public User? Owner { get; set; }
    }
}
