namespace ForumApp.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ForumApp.Common.Enums;
    using ForumApp.Data.Common.Repositories;
    using ForumApp.Data.Models;
    using ForumApp.Services.Mapping;
    using ForumApp.Web.ViewModels.Topics;
    using Microsoft.EntityFrameworkCore;

    public class TopicService : ITopicService
    {
        private readonly IDeletableEntityRepository<Topic> topicRepository;
        private readonly IDeletableEntityRepository<Like> likesRepository;
        private readonly IDeletableEntityRepository<Award> awardsRepository;

        public TopicService(
            IDeletableEntityRepository<Topic> topicRepository,
            IDeletableEntityRepository<Like> likesRepository,
            IDeletableEntityRepository<Award> awardsRepository)
        {
            this.topicRepository = topicRepository;
            this.likesRepository = likesRepository;
            this.awardsRepository = awardsRepository;
        }

        public async Task CreateAsync(CreateTopicInputModel input)
        {
            var topic = new Topic
            {
                Content = input.Content,
                TopicUserId = input.UserId,
            };

            await this.topicRepository.AddAsync(topic);
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var topic = await this.topicRepository.All().FirstOrDefaultAsync(t => t.Id == id);
            this.topicRepository.Delete(topic);
            await this.topicRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TopicInListViewModel>> GetAllAsync(int page, int itemsPerPage = 12)
        {
            return await this.topicRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .Select(x => new TopicInListViewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    TopicUserId = x.TopicUserId,
                    LikesCount = x.Likes.Where(l => l.IsLiked == true).Count(),
                    DayAwards = x.Awards.Where(a => a.AwardType == "DayAward").Count(),
                    MonthAwards = x.Awards.Where(a => a.AwardType == "MonthAward").Count(),
                    YearAwards = x.Awards.Where(a => a.AwardType == "YearAward").Count(),
                })
                .ToListAsync();
        }

        public T GetById<T>(string id)
        {
            return this.topicRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public int GetCount()
        {
            return this.topicRepository.All().Count();
        }

        public async Task<bool> LikeTopic(string id, string userId)
        {
            var userLike = await this.likesRepository.All()
                .FirstOrDefaultAsync(x => x.TopicId == id && x.UserId == userId);

            if (userLike == null)
            {
                var like = new Like
                {
                    IsLiked = true,
                    TopicId = id,
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


        public async Task<bool> DisLikeTopic(string id, string userId)
        {
            var userLike = await this.likesRepository.All()
                .FirstOrDefaultAsync(x => x.TopicId == id && x.UserId == userId);

            if (userLike != null)
            {
                userLike.IsLiked = false;
                await this.likesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task UpdateAsync(string id, EditTopicInputModel input)
        {
            var topic = await this.topicRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            topic.Content = input.Content;

            await this.topicRepository.SaveChangesAsync();
        }

        public async Task<bool> DayAward(string id, string userId)
        {
            var userAward = await this.awardsRepository.All()
                .FirstOrDefaultAsync(x => x.TopicId == id && x.UserId == userId && x.AwardType == "DayAward");

            if (userAward == null)
            {
                var award = new Award
                {
                    AwardType = AwardEnum.DayAward.ToString(),
                    TopicId = id,
                    UserId = userId,
                };

                await this.awardsRepository.AddAsync(award);
                await this.likesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> MonthAward(string id, string userId)
        {
            var userAward = await this.awardsRepository.All()
                .FirstOrDefaultAsync(x => x.TopicId == id && x.UserId == userId && x.AwardType == "MonthAward");

            if (userAward == null)
            {
                var award = new Award
                {
                    AwardType = AwardEnum.MonthAward.ToString(),
                    TopicId = id,
                    UserId = userId,
                };

                await this.awardsRepository.AddAsync(award);
                await this.likesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> YearAward(string id, string userId)
        {
            var userAward = await this.awardsRepository.All()
                .FirstOrDefaultAsync(x => x.TopicId == id && x.UserId == userId && x.AwardType == "YearAward");

            if (userAward == null)
            {
                var award = new Award
                {
                    AwardType = AwardEnum.YearAward.ToString(),
                    TopicId = id,
                    UserId = userId,
                };

                await this.awardsRepository.AddAsync(award);
                await this.likesRepository.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
