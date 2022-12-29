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
        private readonly IDeletableEntityRepository<Like> likesRepository;

        public ReplyService(
            IDeletableEntityRepository<Reply> repliesRepository,
            IDeletableEntityRepository<Like> likesRepository)
        {
            this.repliesRepository = repliesRepository;
            this.likesRepository = likesRepository;
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

        public async Task<bool> LikeTopic(string replyId, string userId)
        {
            var userLike = await this.likesRepository.All()
                .FirstOrDefaultAsync(x => x.ReplyId == replyId && x.UserId == userId);

            if (userLike == null)
            {
                var like = new Like
                {
                    IsLiked = true,
                    ReplyId = replyId,
                    UserId = userId,
                };

                await this.likesRepository.AddAsync(like);
                await this.likesRepository.SaveChangesAsync();
                return true;
            }
            else if (userLike.IsLiked == false)
            {
                userLike.IsLiked = true;
                await this.likesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DisLikeTopic(string replyId, string userId)
        {
            var userLike = await this.likesRepository.All()
                .FirstOrDefaultAsync(x => x.ReplyId == replyId && x.UserId == userId);

            if (userLike != null)
            {
                userLike.IsLiked = false;
                await this.likesRepository.SaveChangesAsync();
                return true;
            }

            return false;
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
