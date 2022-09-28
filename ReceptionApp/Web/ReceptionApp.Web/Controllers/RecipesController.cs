namespace ReceptionApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ReceptionApp.Services;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IRecipeService recipeService;
        private readonly IRecipeTrackerService recipeTrackerService;

        public RecipesController(
            ICategoriesService categoriesService,
            IRecipeService recipeService,
            IRecipeTrackerService recipeTrackerService)
        {
            this.categoriesService = categoriesService;
            this.recipeService = recipeService;
            this.recipeTrackerService = recipeTrackerService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipeModel();
            viewModel.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecipeModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(input);
            }

            await this.recipeService.CreateAsync(input);

            return this.Redirect("/");
        }

        public IActionResult AddFromGotvq()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFromGotvq(AddFromGotvqViewModel model)
        {
            try
            {
                if (!this.ModelState.IsValid)
                {
                    return this.View();
                }

                await this.recipeTrackerService.PopulateDBwithRecipe(model.recipeUrl);
                return this.Redirect("/");
            }
            catch (System.Exception)
            {
                return this.View("NotFormat");
            }
        }
    }
}
