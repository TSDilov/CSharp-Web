namespace SportApp.Web.Controllers
{
    using System;
    using System.Data;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SportApp.Common;
    using SportApp.Data.Models;
    using SportApp.Services.Data;
    using SportApp.Web.ViewModels.GroupTrainings;
    using SportApp.Web.ViewModels.Trainers;

    public class GroupTrainingsController : Controller
    {
        private readonly IGroupTrainingsService groupTrainingsService;
        private readonly UserManager<ApplicationUser> userManager;

        public GroupTrainingsController(
            IGroupTrainingsService groupTrainingsService,
            UserManager<ApplicationUser> userManager)
        {
            this.groupTrainingsService = groupTrainingsService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> All()
        {
            var viewModel = new GroupTrainingsListViewModel
            {
                GroupTrainings = await this.groupTrainingsService.GetAll<GroupTrainingViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> All(string looking)
        {
            var viewModel = new GroupTrainingsListViewModel
            {
                GroupTrainings = await this.groupTrainingsService.GetSearchedTrainings<GroupTrainingViewModel>(looking),
            };

            return this.View(viewModel);
        }

        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateGroupTrainingInputModel();
            return this.View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public async Task<IActionResult> Create(CreateGroupTrainingInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                var user = await this.userManager.GetUserAsync(this.User);
                input.TrainerUserId = user.Id;
                await this.groupTrainingsService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var model = this.groupTrainingsService.GetById<EditGroupTrainingInputModel>(id);
            if (model.TrainerUserId == user.Id)
            {
                return this.View(model);
            }

            return this.RedirectToAction(nameof(this.All));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public async Task<IActionResult> Edit(int id, EditGroupTrainingInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            if (model.TrainerUserId == this.userManager.GetUserId(this.User))
            {
                await this.groupTrainingsService.UpdateAsync(id, model);
                return this.RedirectToAction(nameof(this.All));
            }

            return this.NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = this.groupTrainingsService.GetById<DeleteModel>(id);
            if (model.TrainerUserId == this.userManager.GetUserId(this.User))
            {
                await this.groupTrainingsService.DeleteAsync(id);
                return this.RedirectToAction(nameof(this.All));
            }

            return this.NotFound();
        }

        [Authorize]
        public async Task<IActionResult> SignForTraining(int id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.groupTrainingsService.SignInForTraining(id, user.Id);
            return this.RedirectToAction(nameof(this.All));
        }

        [Authorize(Roles = GlobalConstants.TrainerRoleName)]
        public async Task<IActionResult> SignInUsers(int id)
        {
            var bookedUsers = await this.groupTrainingsService.SighnInUsers(id);
            return this.View(bookedUsers);
        }
    }
}
