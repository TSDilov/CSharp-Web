namespace ForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ForumApp.Data.Common.Repositories;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;
    using ForumApp.Web.ViewModels.Replies;
    using Microsoft.EntityFrameworkCore;

    public class ReplyService : IReplyService
    {
        private readonly IDeletableEntityRepository<Reply> repliesRepository;

        public ReplyService(IDeletableEntityRepository<Reply> repliesRepository)
        {
            this.repliesRepository = repliesRepository;
        }

        public async Task CreateAsync(ReplayInputModel input)
        {
            var reply = new Reply
            {
                Content = input.Content,
                TopicId = input.TopicId,
                UserId = input.UserId,
            };

            await this.repliesRepository.AddAsync(reply);
            await this.repliesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string topicId)
        {
            return await this.repliesRepository.AllAsNoTracking()
                .Where(x => x.TopicId == topicId)
                .To<T>()
                .ToListAsync();
        }
    }
}
