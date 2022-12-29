namespace ForumApp.Web.ViewModels.Topics
{
    using AutoMapper;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;
    using System.Linq;

    public class TopicInListViewModel : IMapFrom<Topic>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string TopicUserId { get; set; }

        public int LikesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Topic, TopicInListViewModel>()
                .ForMember(x => x.LikesCount, opt =>
                opt.MapFrom(x => x.Likes.Where(l => l.IsLiked == true).Count()));
        }
    }
}