namespace ReceptionApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ReceptionApp.Services.Data;
    using ReceptionApp.Web.ViewModels.Recipes;

    public class RecipesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public RecipesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Create()
        {
            var viewModel = new CreateRecipeModel();
            viewModel.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateRecipeModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CategoryItems = this.categoriesService.GetAllCategoriesAsKeyValuePairs();
                return this.View(input);
            }

            return this.Redirect("/");
        }
    }
}
