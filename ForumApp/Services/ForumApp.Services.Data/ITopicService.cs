namespace ForumApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ForumApp.Web.ViewModels.Topics;

    public interface ITopicService
    {
        Task CreateAsync(CreateTopicInputModel input);

        Task<IEnumerable<TopicInListViewModel>> GetAllAsync(int page, int itemsPerPage = 12);

        int GetCount();

        T GetById<T>(string id);

        Task UpdateAsync(string id, EditTopicInputModel input);

        Task DeleteAsync(string id);

        Task<bool> LikeTopic(string id, string userId);

        Task<bool> DisLikeTopic(string id, string userId);

        Task<bool> DayAward(string id, string userId);

        Task<bool> MonthAward(string id, string userId);

        Task<bool> YearAward(string id, string userId);
    }
}
