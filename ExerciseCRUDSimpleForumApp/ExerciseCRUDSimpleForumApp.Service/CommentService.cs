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
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext dbContext;

        public CommentService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Task CreateAsync(CreateCommentViewModel model)
        {
            var comment = new Comment
            {
                Content = model.Content,
                PostId = model.PostId,
                AddedByUserId = model.AddedByUserId,
                Username = model.Username,
            };

            this.dbContext.Comments.Add(comment);
            this.dbContext.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
