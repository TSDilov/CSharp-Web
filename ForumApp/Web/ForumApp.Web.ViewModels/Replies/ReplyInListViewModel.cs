namespace ForumApp.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;

    public class ReplyInListViewModel : IMapFrom<Reply>
    {
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string TopicId { get; set; }

        public string UserId { get; set; }
    }
}