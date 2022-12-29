namespace ForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumApp.Web.ViewModels.Replies;

    public interface IReplyService
    {
        Task CreateAsync(ReplayInputModel input);

        Task<IEnumerable<T>> GetAllAsync<T>(string topicId);
    }
}
