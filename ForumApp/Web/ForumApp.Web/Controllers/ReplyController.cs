namespace ForumApp.Web.Controllers
{
    using System.Threading.Tasks;

    using ForumApp.Data.Models;
    using ForumApp.Services.Data;
    using ForumApp.Web.ViewModels.Replies;
    using ForumApp.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReplyController : Controller
    {
        private readonly IReplyService replyService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReplyController(
            IReplyService replyService,
            UserManager<ApplicationUser> userManager)
        {
            this.replyService = replyService;
            this.userManager = userManager;
        }

        [Authorize]
        public async Task<IActionResult> All(string id)
        {
            var viewModel = new ReplyListViewModel
            {
                Replies = await this.replyService.GetAllAsync<ReplyInListViewModel>(id),
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(string replyId, string userId)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                var model = this.replyService.GetById<ReplyInputModel>(replyId);
                if (model == null)
                {
                    return this.RedirectToAction(nameof(this.All));
                }

                return this.View(model);
            }

            return this.Redirect("/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(string replyId, string userId, ReplyInputModel model)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                await this.replyService.UpdateAsync(replyId, model);
            }

            return this.RedirectToAction(nameof(this.All), "Topic");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string replyId, string userId)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                await this.replyService.DeleteAsync(replyId);
            }

            return this.RedirectToAction(nameof(this.All), "Topic");
        }
    }
}
