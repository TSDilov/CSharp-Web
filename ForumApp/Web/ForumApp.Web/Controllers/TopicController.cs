namespace ForumApp.Web.Controllers
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    using ForumApp.Common;
    using ForumApp.Data.Models;
    using ForumApp.Services.Data;
    using ForumApp.Web.ViewModels.Replies;
    using ForumApp.Web.ViewModels.Topics;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class TopicController : Controller
    {
        private readonly ITopicService topicService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IReplyService replyService;

        public TopicController(
            ITopicService topicService,
            UserManager<ApplicationUser> userManager,
            IReplyService replyService)
        {
            this.topicService = topicService;
            this.userManager = userManager;
            this.replyService = replyService;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            const int ItemsPerPage = 12;
            var viewModel = new TopicsListViewModel
            {
                PageNumber = id,
                Topics = await this.topicService.GetAllAsync(id, ItemsPerPage),
                TopicsCount = this.topicService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateTopicInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateTopicInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                input.UserId = user.Id;
                await this.topicService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult Edit(string id, string userId)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                var model = this.topicService.GetById<EditTopicInputModel>(id);
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
        public async Task<IActionResult> Edit(string id, string userId, EditTopicInputModel model)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                await this.topicService.UpdateAsync(id, model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(string id, string userId)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                await this.topicService.DeleteAsync(id);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public IActionResult Reply(string id, string userId)
        {
            var viewModel = new ReplyInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Reply(string id, string userId, ReplyInputModel model)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View(model);
                }

                model.TopicId = id;
                model.UserId = userId;
                await this.replyService.CreateAsync(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> Like(string id, string userId)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                return this.RedirectToAction(nameof(this.OwnTopic));
            }

            var result = await this.topicService.LikeTopic(id, this.userManager.GetUserId(this.User));
            if (!result)
            {
                return this.RedirectToAction(nameof(this.AlreadyLike));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> DisLike(string id, string userId)
        {
            if (this.userManager.GetUserId(this.User) == userId)
            {
                return this.RedirectToAction(nameof(this.OwnTopic));
            }

            var result = await this.topicService.DisLikeTopic(id, this.userManager.GetUserId(this.User));
            if (!result)
            {
                return this.RedirectToAction(nameof(this.AlreadyLike));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> DayAward(string id, string userId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (currentUser.Id == userId)
            {
                return this.RedirectToAction(nameof(this.OwnTopic));
            }

            var result = await this.topicService.DayAward(id, currentUser.Id);
            if (!result)
            {
                return this.RedirectToAction(nameof(this.AlreadyGaveAward));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> MonthAward(string id, string userId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (currentUser.Id == userId)
            {
                return this.RedirectToAction(nameof(this.OwnTopic));
            }

            var result = await this.topicService.MonthAward(id, currentUser.Id);
            if (!result)
            {
                return this.RedirectToAction(nameof(this.AlreadyGaveAward));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize]
        public async Task<IActionResult> YearAward(string id, string userId)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            if (currentUser.Id == userId)
            {
                return this.RedirectToAction(nameof(this.OwnTopic));
            }

            var result = await this.topicService.YearAward(id, currentUser.Id);
            if (!result)
            {
                return this.RedirectToAction(nameof(this.AlreadyGaveAward));
            }

            return this.RedirectToAction(nameof(this.All));
        }

        public IActionResult AlreadyLike()
        {
            return this.View();
        }

        public IActionResult OwnTopic()
        {
            return this.View();
        }

        public IActionResult AlreadyGaveAward()
        {
            return this.View();
        }
    }
}
