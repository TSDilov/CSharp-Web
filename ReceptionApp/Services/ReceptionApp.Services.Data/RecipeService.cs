namespace ReceptionApp.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using ReceptionApp.Data.Common.Repositories;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Web.ViewModels.Recipes;

    public class RecipeService : IRecipeService
    {
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public RecipeService(
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.recipesRepository = recipesRepository;
            this.ingredientRepository = ingredientRepository;
        }

        public async Task CreateAsync(CreateRecipeModel input)
        {
            var recipe = new Recipe
            {
                CategoryId = input.CategoryId,
                Name = input.Name,
                CookingTIme = TimeSpan.FromMinutes(input.CookingTime),
                Instructions = input.Instructions,
                PortionCount = input.PortionCount,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
            };

            foreach (var ingredient in input.Ingredients)
            {
                var ingredientInDb = this.ingredientRepository.All().FirstOrDefault(x => x.Name == ingredient.Name);
                if (ingredientInDb == null)
                {
                    ingredientInDb = new Ingredient { Name = ingredient.Name };
                }

                recipe.Ingredients.Add(new RecipeIngredient
                {
                    Ingredient = ingredientInDb,
                    Quantity = ingredient.Quantity,
                });
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
