using ExerciseCRUDSimpleForumApp.Data;
using ExerciseCRUDSimpleForumApp.Data.Model;
using ExerciseCRUDSimpleForumApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseCRUDSimpleForumApp.Service
{
    public class PostService : IPostService
    {
        private readonly ApplicationDbContext dbContext;

        public PostService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task CreateAsync(CreatePostViewModel post)
        {
            var newPost = new Post 
            {
                Title = post.Title,
                Content = post.Content,
            };

            this.dbContext.Posts.Add(newPost);
            this.dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var post = this.GetById(id);
            post.IsDeleted = true;
            this.dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task EditAsync(CreatePostViewModel post, int id)
        {
            var postFromBase = this.GetById(id);
            postFromBase.Title = post.Title;
            postFromBase.Content = post.Content;
            this.dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<Post> GetAll()
        {
            return this.dbContext.Posts
                .Where(x => x.IsDeleted == false)
                .ToList();
        }

        public Post GetById(int id)
        {
            return this.dbContext.Posts.FirstOrDefault(x => x.Id == id);
        }
    }
}
