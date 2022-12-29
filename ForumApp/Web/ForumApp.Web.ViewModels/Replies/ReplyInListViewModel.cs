using ForumApp.Data.Models;
using ForumApp.Services.Mapping;
using System.ComponentModel.DataAnnotations;

namespace ForumApp.Web.ViewModels.Replies
{
    public class ReplyInListViewModel : IMapFrom<Reply>
    {
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string TopicId { get; set; }

        public string UserId { get; set; }
    }
}