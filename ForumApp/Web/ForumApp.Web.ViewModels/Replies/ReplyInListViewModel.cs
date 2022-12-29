namespace ForumApp.Web.ViewModels.Replies
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;
    using ForumApp.Web.ViewModels.Topics;

    public class ReplyInListViewModel : IMapFrom<Reply>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        public string Content { get; set; }

        public string TopicId { get; set; }

        public string UserId { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Reply, ReplyInListViewModel>()
                .ForMember(x => x.LikesCount, opt =>
                opt.MapFrom(x => x.Likes.Where(l => l.IsLiked == true).Count()));
        }
    }
}
