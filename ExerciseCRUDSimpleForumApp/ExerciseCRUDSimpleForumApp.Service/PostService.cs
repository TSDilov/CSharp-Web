using ExerciseCRUDSimpleForumApp.Data;
using ExerciseCRUDSimpleForumApp.Data.Model;
using ExerciseCRUDSimpleForumApp.Data.Repositories;
using ExerciseCRUDSimpleForumApp.ViewModels;
using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository repository;

        public PostService(
            ApplicationDbContext dbContext,
            IRepository repository
            )
        {
            this.dbContext = dbContext;
            this.repository = repository;
        }

        public async Task CreateAsync(CreatePostViewModel post)
        {
            var newPost = new Post 
            {
                Title = post.Title,
                Content = post.Content,
                AddedByUserId = post.AddedByUserId,
                Username = post.Username,
            };

            await this.repository.AddAsync(newPost);
            await this.repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var post = this.repository.All<Post>()
                .FirstOrDefault(x => x.Id == id);

            if (post!= null)
            {
                post.IsDeleted = true;
                await this.repository.SaveChangesAsync();
            }
        }

        public Task EditAsync(CreatePostViewModel post, int id)
        {
            var postFromBase = this.GetById(id);
            postFromBase.Title = post.Title;
            postFromBase.Content = post.Content;
            this.dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public IEnumerable<PostViewModel> GetAll()
        {
            var postsForTheApp = new List<PostViewModel>();
            var postFromBase =  this.dbContext.Posts
                .Where(x => x.IsDeleted == false)
                .ToList();

            foreach (var post in postFromBase)
            {
                var postForTheApp = new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    AddedByUserId = post.AddedByUserId,
                    UserName = post.Username,
                };

                var commentsFromBase = this.dbContext.Comments
                    .Where(x => x.PostId == post.Id)
                    .ToList();

                foreach (var comment in commentsFromBase)
                {
                    postForTheApp.Comments?.Add(new CommentViewModel
                    {
                        Id = comment.Id,
                        Content = comment.Content,
                        PostId = comment.PostId,
                        AddedByUserId = comment.AddedByUserId,
                        Username = comment.Username,
                    });
                }
                
                postsForTheApp.Add(postForTheApp);
            }

            return postsForTheApp;          
        }

        public Post GetById(int id)
        {
            return this.dbContext.Posts.FirstOrDefault(x => x.Id == id);
        }
    }
}
