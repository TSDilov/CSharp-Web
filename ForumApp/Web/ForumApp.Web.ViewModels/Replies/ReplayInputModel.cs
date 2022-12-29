namespace ForumApp.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    public class ReplayInputModel
    {
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; set; }

        public string TopicId { get; set; }

        public string UserId { get; set; }
    }
}
