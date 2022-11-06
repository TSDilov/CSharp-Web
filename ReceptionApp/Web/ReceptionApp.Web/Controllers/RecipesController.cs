namespace ReceptionApp.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;

    using AngleSharp.Html;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using ReceptionApp.Common;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipeService recipeService,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
            this.userManager = userManager;
            this.environment = environment;
        }

        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new CreateRecipeModel();
            viewModel.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateRecipeModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            try
            {
                await this.recipeService.CreateAsync(input, user.Id, $"{this.environment.WebRootPath}/images");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }

        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public IActionResult Edit(int id)
        {
            var model = this.recipeService.GetById<EditRecipeInputModel>(id);
            model.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Edit(int id, EditRecipeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(model);
            }

            await this.recipeService.UpdateAsync(id, model);
            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        public IActionResult All(int id = 1)
        {
            const int ItemsPerPage = 12;
            var viewModel = new RecipesListViewModel
            {
                PageNumber = id,
                Recipes = this.recipeService.GetAll<RecipeInListViewModel>(id, ItemsPerPage),
                RecipesCount = this.recipeService.GetCount(),
                ItemsPerPage = ItemsPerPage,
            };
            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var recipe = this.recipeService.GetById<SingleRecipeViewModel>(id);
            return this.View(recipe);
        }

        [HttpPost]
        [Authorize(Roles =GlobalConstants.AdministratorRoleName)]
        public async Task<IActionResult> Delete(int id)
        {
            await this.recipeService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.All));
        }
    }
}
