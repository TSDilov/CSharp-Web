namespace ForumApp.Data.Models
{
    using ForumApp.Data.Common.Models;

    public class Like : BaseDeletableModel<int>
    {
        public bool IsLiked { get; set; }

        public string TopicId { get; set; }

        public Topic Topic { get; set; }

        public string ReplyId { get; set; }

        public Reply Reply { get; set; }

        public string UserId { get; set; }
    }
}
