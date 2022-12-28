namespace ForumApp.Data.Models
{
    using ForumApp.Data.Common.Models;

    public class Award : BaseDeletableModel<int>
    {
        public string AwardType { get; set; }

        public string TopicId { get; set; }

        public Topic Topic { get; set; }
    }
}
