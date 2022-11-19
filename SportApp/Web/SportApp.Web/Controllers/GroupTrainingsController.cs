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
                await this.groupTrainingsService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.Redirect("/");
        }
    }
}
