namespace ForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ForumApp.Web.ViewModels.Replies;
    using ForumApp.Web.ViewModels.Topics;

    public interface IReplyService
    {
        Task CreateAsync(ReplyInputModel input);

        Task<IEnumerable<T>> GetAllAsync<T>(string topicId);

        Task UpdateAsync(string replyId, ReplyInputModel input);

        Task DeleteAsync(string replyId);

        T GetById<T>(string replyId);
    }
}
