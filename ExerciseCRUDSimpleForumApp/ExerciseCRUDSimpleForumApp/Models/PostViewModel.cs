using System.ComponentModel.DataAnnotations;

namespace ExerciseCRUDSimpleForumApp.Web.Models
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
