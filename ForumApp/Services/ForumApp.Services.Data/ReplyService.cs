namespace ForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ForumApp.Data.Common.Repositories;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;
    using ForumApp.Web.ViewModels.Replies;
    using ForumApp.Web.ViewModels.Topics;
    using Microsoft.EntityFrameworkCore;

    public class ReplyService : IReplyService
    {
        private readonly IDeletableEntityRepository<Reply> repliesRepository;

        public ReplyService(IDeletableEntityRepository<Reply> repliesRepository)
        {
            this.repliesRepository = repliesRepository;
        }

        public async Task CreateAsync(ReplyInputModel input)
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

        public async Task DeleteAsync(string replyId)
        {
            var topic = await this.repliesRepository.All().FirstOrDefaultAsync(t => t.Id == replyId);
            this.repliesRepository.Delete(topic);
            await this.repliesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string topicId)
        {
            return await this.repliesRepository.AllAsNoTracking()
                .Where(x => x.TopicId == topicId)
                .To<T>()
                .ToListAsync();
        }

        public T GetById<T>(string replyId)
        {
            return this.repliesRepository.AllAsNoTracking()
                .Where(x => x.Id == replyId)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task UpdateAsync(string replyId, ReplyInputModel input)
        {
            var reply = await this.repliesRepository.All()
                .FirstOrDefaultAsync(x => x.Id == replyId);

            reply.Content = input.Content;

            await this.repliesRepository.SaveChangesAsync();
        }
    }
}
