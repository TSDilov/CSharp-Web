namespace ForumApp.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;

    public class ReplyInputModel : IMapFrom<Reply>
    {
        public string Id { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Content { get; set; }

        public string TopicId { get; set; }

        public string UserId { get; set; }
    }
}
