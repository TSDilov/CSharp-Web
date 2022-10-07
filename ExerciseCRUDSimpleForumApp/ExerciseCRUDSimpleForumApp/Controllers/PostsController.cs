using ExerciseCRUDSimpleForumApp.Service;
using ExerciseCRUDSimpleForumApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseCRUDSimpleForumApp.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;

        public PostsController(IPostService postService)
        {
            this.postService = postService;
        }

        public IActionResult All()
        {
            var posts = new List<PostViewModel>();
            var postsFromBase = this.postService.GetAll();

            foreach (var post in postsFromBase)
            {
                var postForView = new PostViewModel 
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                };

                posts.Add(postForView);
            }
            return View(posts);
        }
    }
}
