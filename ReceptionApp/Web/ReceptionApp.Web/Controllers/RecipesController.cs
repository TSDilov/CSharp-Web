namespace ReceptionApp.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipeService recipeService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
            this.userManager = userManager;
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
            await this.recipeService.CreateAsync(input, user.Id);

            return this.Redirect("/");
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
    }
}
