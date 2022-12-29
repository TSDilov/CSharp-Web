namespace ForumApp.Web.Controllers
{
    using ForumApp.Services.Data;
    using ForumApp.Web.ViewModels.Replies;
    using ForumApp.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class ReplyController : Controller
    {
        private readonly IReplyService replyService;

        public ReplyController(IReplyService replyService)
        {
            this.replyService = replyService;
        }

        [Authorize]
        public async Task<IActionResult> All(string id, string userId)
        {
            var viewModel = new ReplyListViewModel
            {
                Replies = await this.replyService.GetAllAsync<ReplyInListViewModel>(id),
            };

            return this.View(viewModel);
        }
    }
}
