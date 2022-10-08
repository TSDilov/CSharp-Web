using System.ComponentModel.DataAnnotations;

namespace ExerciseCRUDSimpleForumApp.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string? Title { get; set; }

        [Required]
        [StringLength(1500, MinimumLength = 25)]
        public string? Content { get; set; }

        public string? AddedByUserId { get; set; }

        public string? Username { get; set; }
    }
}
