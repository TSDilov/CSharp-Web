namespace ForumApp.Web.ViewModels.Topics
{
    using System.ComponentModel.DataAnnotations;

    public class CreateTopicInputModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; set; }

        public string UserId { get; set; }
    }
}
