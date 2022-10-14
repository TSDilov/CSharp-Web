using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ExerciseTaskBoardApp.Data.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(DataConstants.MaxUserFirstName)]
        public string? FirstName { get; init; }


        [Required]
        [MaxLength(DataConstants.MaxUserLastName)]
        public string? LastName { get; init; }
    }
}
