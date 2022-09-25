namespace ReceptionApp.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Globalization;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using AngleSharp;
    using ReceptionApp.Data.Common.Repositories;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services.Models;

    public class RecipeTrackerService : IRecipeTrackerService
    {
        private readonly IConfiguration config;
        private readonly IBrowsingContext context;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly IRepository<Image> imagesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientsRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IRepository<RecipeIngredient> ingredientRecipeRepository;

        public RecipeTrackerService(
            IDeletableEntityRepository<Category> categoriesRepository,
            IRepository<Image> imagesRepository,
            IDeletableEntityRepository<Ingredient> ingredientsRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IRepository<RecipeIngredient> ingredientRecipeRepository)
        {
            this.config = Configuration.Default.WithDefaultLoader();
            this.context = BrowsingContext.New(this.config);
            this.categoriesRepository = categoriesRepository;
            this.imagesRepository = imagesRepository;
            this.ingredientsRepository = ingredientsRepository;
            this.recipesRepository = recipesRepository;
            this.ingredientRecipeRepository = ingredientRecipeRepository;
        }

        public async Task PopulateDBwithRecipe(string url)
        {
            try
            {
                var recipe = this.GetRecipe(url);
                var recipeForBase = this.recipesRepository.All().FirstOrDefault(x => x.Name == recipe.RecipeName);
                if (recipeForBase != null)
                {
                    return;
                }

                recipeForBase = new Recipe
                {
                    Name = recipe.RecipeName,
                    Instructions = recipe.Instructions,
                    PreparationTime = recipe.PreparationTime,
                    CookingTIme = recipe.CookingTime,
                    PortionCount = recipe.PortionCount,
                    OriginalUrl = recipe.OriginalUrl,
                };

                foreach (var ingredient in recipe.Ingredients)
                {
                    var ingredientInDb = this.ingredientsRepository.All().FirstOrDefault(x => x.Name == ingredient.Key);
                    if (ingredientInDb == null)
                    {
                        ingredientInDb = new Ingredient { Name = ingredient.Key };
                        await this.ingredientsRepository.AddAsync(ingredientInDb);
                    }

                    var recipeIngredientForBase = new RecipeIngredient
                    {
                        IngredientId = ingredientInDb.Id,
                        RecipeId = recipeForBase.Id,
                        Quantity = ingredient.Value,
                    };

                    await this.ingredientRecipeRepository.AddAsync(recipeIngredientForBase);
                }

                var categoryInBase = this.categoriesRepository.All().FirstOrDefault(x => x.Name == recipe.CategoryName);
                if (categoryInBase == null)
                {
                    categoryInBase = new Category { Name = recipe.CategoryName };
                    await this.categoriesRepository.AddAsync(categoryInBase);
                }

                recipeForBase.Category = categoryInBase;

                var image = new Image
                {
                    Extension = recipe.Extension,
                    Recipe = recipeForBase,
                };

                await this.imagesRepository.AddAsync(image);

                await this.recipesRepository.AddAsync(recipeForBase);
                await this.recipesRepository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private RecipeDto GetRecipe(string url)
        {
            var document = this.context.OpenAsync(url).GetAwaiter().GetResult();

            if (document.StatusCode == HttpStatusCode.NotFound)
            {
                throw new InvalidOperationException();
            }

            var recipe = new RecipeDto();
            recipe.OriginalUrl = url;
            var category = document.QuerySelector("div > nav > p > a:nth-child(5)");
            recipe.CategoryName = category.TextContent;
            var recipeName = document.QuerySelector("div > nav > p > span.last");
            recipe.RecipeName = recipeName.TextContent;
            var instructions = document.QuerySelector("div > ol");
            recipe.Instructions = instructions.TextContent;
            var preparationTime = document.QuerySelector("div > p:nth-child(6)").TextContent
                .Replace("Време за подготовка: – ", string.Empty)
                .Replace(" мин.", string.Empty);
            recipe.PreparationTime = TimeSpan.ParseExact(preparationTime, "mm", CultureInfo.InvariantCulture);
            var cookingTime = document.QuerySelector("div > p:nth-child(7)").TextContent
                .Replace("Време за готвене: – ", string.Empty)
                .Replace(" мин.", string.Empty);
            recipe.CookingTime = TimeSpan.ParseExact(cookingTime, "mm", CultureInfo.InvariantCulture);
            var portionsCount = document.QuerySelector("div > p:nth-child(4)").TextContent
                .Replace("Порции: ", string.Empty);
            recipe.PortionCount = int.Parse(portionsCount);
            var image = document.QuerySelector("div > picture > img").GetAttribute("src");
            recipe.Extension = image;
            var ingredients = document.QuerySelectorAll(".elementor-widget-theme-post-content > div > ul > li");

            foreach (var ingredient in ingredients)
            {
                var ingredientInfo = ingredient.TextContent.Split(" ");
                var quantity = string.Empty;
                var name = string.Empty;
                if (ingredientInfo[1] == "гр." || ingredientInfo[1] == "бр.")
                {
                    quantity = ingredientInfo[0] + ingredientInfo[1];
                    for (int i = 2; i < ingredientInfo.Length; i++)
                    {
                        name += ingredientInfo[i] + " ";
                    }
                }
                else
                {
                    try
                    {
                        var quantityInfo = ingredientInfo[0].Replace(".", string.Empty);
                        if (char.IsDigit(char.Parse(quantityInfo)))
                        {
                            quantity = quantityInfo;
                            for (int i = 1; i < ingredientInfo.Length; i++)
                            {
                                name += ingredientInfo[i] + " ";
                            }
                        }
                    }
                    catch (Exception)
                    {
                        name = ingredient.TextContent;
                    }
                }

                recipe.Ingredients.Add(name, quantity);
            }

            return recipe;
        }
    }
}
