namespace ForumApp.Web.ViewModels.Topics
{
    using AutoMapper;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;
    using System.Linq;

    public class TopicInListViewModel
    {
        public string Id { get; set; }

        public string Content { get; set; }

        public string TopicUserId { get; set; }

        public int LikesCount { get; set; }

        public int DayAwards { get; set; }

        public int MonthAwards { get; set; }

        public int YearAwards { get; set; }

        public int RepliesCount { get; set; }
    }
}
