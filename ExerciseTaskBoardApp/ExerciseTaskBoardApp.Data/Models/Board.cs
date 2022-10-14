using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel.DataAnnotations;

namespace ExerciseTaskBoardApp.Data.Models
{
    public class Board
    {
        public Board()
        {
            this.Tasks = new HashSet<TaskA>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(DataConstants.MaxBoardName)]
        public string? Name { get; set; }

        public ICollection<TaskA> Tasks { get; set; }
    }
}