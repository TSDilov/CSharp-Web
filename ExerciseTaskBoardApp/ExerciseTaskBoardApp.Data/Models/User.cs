using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ExerciseTaskBoardApp.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.Tasks = new HashSet<TaskA>();
        }

        [Required]
        [MaxLength(DataConstants.MaxUserFirstName)]
        public string? FirstName { get; init; }


        [Required]
        [MaxLength(DataConstants.MaxUserLastName)]
        public string? LastName { get; init; }

        public ICollection<TaskA> Tasks { get; set; }
    }
}
