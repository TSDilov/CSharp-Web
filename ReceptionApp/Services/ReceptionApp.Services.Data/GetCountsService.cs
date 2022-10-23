namespace ReceptionApp.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using ReceptionApp.Data.Common.Repositories;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Web.ViewModels.Home;

    public class GetCountsService : IGetCountsService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IRecipeService recipeService;

        public GetCountsService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IRecipeService recipeService)
        {
            this.categoriesRepository = categoriesRepository;
            this.imagesRepository = imagesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesRepository = recipesRepository;
            this.recipeService = recipeService;
        }

        public IndexViewModel GetCounts()
        {
            var viewModel = new IndexViewModel
            {
                CategoriesCount = this.categoriesRepository.All().Count(),
                ImagesCount = this.imagesRepository.All().Count(),
                IngredientsCount = this.ingredientsRepository.All().Count(),
                RecipesCount = this.recipesRepository.All().Count(),
                RandomRecipies = this.recipeService.GetRandom<IndexPageRecipeViewModel>(10),
            };

            return viewModel;
        }
    }
}
