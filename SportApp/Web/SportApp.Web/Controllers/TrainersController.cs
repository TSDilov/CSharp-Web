namespace SportApp.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using SportApp.Common;
    using SportApp.Data.Models;
    using SportApp.Services.Data;
    using SportApp.Web.ViewModels.Trainers;

    public class TrainersController : Controller
    {
        private readonly ITrainerService trainerService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public TrainersController(
            ITrainerService trainerService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.trainerService = trainerService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
            this.environment = environment;
        }

        public async Task<IActionResult> All(int id = 1)
        {
            const int ItemsPerPage = 12;
            var viewModel = new TrainersListViewModel
            {
                PageNumber = id,
                Trainers = await this.trainerService.GetAll<TrainerInListViewModel>(id, ItemsPerPage),
                TrainersCount = this.trainerService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };

            return this.View(viewModel);
        }

        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public IActionResult Create()
        {
            var viewModel = new CreateTrainerInputModel();
            viewModel.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Create(CreateTrainerInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.trainerService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }

        [Authorize]
        public IActionResult ById(int id)
        {
            var trainer = this.trainerService.GetById<SingleTrainerViewModel>(id);
            return this.View(trainer);
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var model = this.trainerService.GetById<EditTrainerInputModel>(id);
            model.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditTrainerInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(model);
            }

            await this.trainerService.UpdateAsync(id, model);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.trainerService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
