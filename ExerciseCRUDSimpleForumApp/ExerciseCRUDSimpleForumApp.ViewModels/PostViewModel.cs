using System.ComponentModel.DataAnnotations;

namespace ExerciseCRUDSimpleForumApp.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Content { get; set; }
    }
}
