using ExerciseCRUDSimpleForumApp.Data.Model;
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

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var modelForBase = new Post
            {
                Title = model.Title,
                Content = model.Content,
            };

            await this.postService.CreateAsync(modelForBase);
            return this.Redirect("/");
        }

        public IActionResult Edit(int id)
        {
            var post = this.postService.GetById(id);
            return View(new CreatePostViewModel() 
            {
                Title = post.Title,
                Content=post.Content,
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var postFromBase = this.postService.GetById(id);
            postFromBase.Title = model.Title;
            postFromBase.Content = model.Content;
            await this.postService.EditAsync(postFromBase, id);
            this.postService.GetAll();

            return RedirectToAction("All");
        }
    }
}
