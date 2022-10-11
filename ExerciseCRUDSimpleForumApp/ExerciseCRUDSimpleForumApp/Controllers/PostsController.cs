using ExerciseCRUDSimpleForumApp.Data.Model;
using ExerciseCRUDSimpleForumApp.Service;
using ExerciseCRUDSimpleForumApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseCRUDSimpleForumApp.Web.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly UserManager<IdentityUser> userManager;

        public PostsController(
            IPostService postService,
            UserManager<IdentityUser> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }

        public IActionResult All()
        {
            var posts = this.postService.GetAll();
            return View(posts);
        }

        public async Task<IActionResult> Add()
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

            var user = await this.userManager.GetUserAsync(this.User);
            model.AddedByUserId = user.Id;
            model.Username = user.UserName;

            await this.postService.CreateAsync(model);

            this.TempData["Message"] = "Added new post!";
            return this.RedirectToAction("All");
        }

        public IActionResult Edit(int id)
        {
            var postFromBase = this.postService.GetById(id);
            return View(new CreatePostViewModel()
            {
                Title = postFromBase.Title,
                Content = postFromBase.Content,
                AddedByUserId = postFromBase.AddedByUserId
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CreatePostViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            await this.postService.EditAsync(model, id);
            return RedirectToAction("All");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.postService.DeleteAsync(id);
            return RedirectToAction("All");
        }
    }
}
