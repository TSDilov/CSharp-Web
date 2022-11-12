using ExerciseCRUDSimpleForumApp.Service;
using ExerciseCRUDSimpleForumApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ExerciseCRUDSimpleForumApp.Web.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService commentService;
        private readonly UserManager<IdentityUser> userManager;

        public CommentsController(
            ICommentService commentService,
            UserManager<IdentityUser> userManager)
        {
            this.commentService = commentService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(int id, CreateCommentViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            model.PostId = id;
            model.AddedByUserId = user.Id;
            model.Username = user.UserName;

            await this.commentService.CreateAsync(model);
            return this.Redirect("/Posts/All");
        }
    }
}
