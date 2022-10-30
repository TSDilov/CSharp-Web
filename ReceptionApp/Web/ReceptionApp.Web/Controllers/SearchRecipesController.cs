namespace ReceptionApp.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels.Recipes;
    using ReceptionApp.Web.ViewModels.SearchRecuipes;

    public class SearchRecipesController : Controller
    {
        private readonly IRecipeService recipeService;
        private readonly IIngredientsService ingredientService;

        public SearchRecipesController(
            IRecipeService recipeService,
            IIngredientsService ingredientService)
        {
            this.recipeService = recipeService;
            this.ingredientService = ingredientService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new SearchIndexViewModel
            {
                Ingredients = await this.ingredientService.GetALlAsync<IngredientIdNameViewModel>(),
            };

            return this.View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> List(SearchListInputModel model)
        {
            var viewModel = new ListViewModel
            {
                Recipes = await this.recipeService.GetByIngredientsAsync<RecipeInListViewModel>(model.Ingredients),
            };

            return this.View(viewModel);
        }
    }
}
