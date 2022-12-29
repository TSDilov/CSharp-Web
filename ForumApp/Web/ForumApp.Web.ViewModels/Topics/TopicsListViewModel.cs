namespace ForumApp.Web.ViewModels.Topics
{
    using System.Collections.Generic;

    public class TopicsListViewModel : PagingViewModel
    {
        public IEnumerable<TopicInListViewModel> Topics { get; set; }
    }
}
