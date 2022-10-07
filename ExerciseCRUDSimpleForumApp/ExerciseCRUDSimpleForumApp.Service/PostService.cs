using ExerciseCRUDSimpleForumApp.Data;
using ExerciseCRUDSimpleForumApp.Data.Model;
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

        public IEnumerable<Post> GetAll()
        {
            return this.dbContext.Posts.ToList();
        }
    }
}
