namespace ReceptionApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using ReceptionApp.Data.Common.Repositories;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services.Mapping;
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

        public async Task CreateAsync(CreateRecipeModel input, string userId, string imagePath)
        {
            var recipe = new Recipe
            {
                CategoryId = input.CategoryId,
                Name = input.Name,
                CookingTIme = TimeSpan.FromMinutes(input.CookingTime),
                Instructions = input.Instructions,
                PortionsCount = input.PortionCount,
                PreparationTime = TimeSpan.FromMinutes(input.PreparationTime),
                AddedByUserID = userId,
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

            var allowedExtensions = new[] { "jpg", "png", "gif" };
            Directory.CreateDirectory($"{imagePath}/recipes/");
            foreach (var image in input.Images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');
                if (!allowedExtensions.Contains(extension))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                var dbImage = new Image
                {
                    AddedByUserID = userId,
                    Extension = extension,
                };

                recipe.Images.Add(dbImage);
                var physicalPath = $"{imagePath}/recipes/{dbImage.Id}.{extension}";
                using (Stream fileStream = new FileStream(physicalPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.recipesRepository.AddAsync(recipe);
            await this.recipesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            return this.recipesRepository.AllAsNoTracking()
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * itemsPerPage)
                .Take(itemsPerPage)
                .To<T>()
                .ToList();
        }

        public T GetById<T>(int id)
        {
            return this.recipesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>()
                .FirstOrDefault();
        }

        public async Task<IEnumerable<T>> GetByIngredientsAsync<T>(IEnumerable<int> ingredientIds)
        {
            var query = this.recipesRepository.All().AsQueryable();
            foreach (var ingredientId in ingredientIds)
            {
                query = query.Where(x => x.Ingredients.Any(i => i.IngredientId == ingredientId));
            }

            return await query.To<T>().ToListAsync();
        }

        public int GetCount()
        {
            return this.recipesRepository.All().Count();
        }

        public IEnumerable<T> GetRandom<T>(int count)
        {
            return this.recipesRepository.All()
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .To<T>()
                .ToList();
        }

        public async Task UpdateAsync(int id, EditRecipeInputModel input)
        {
            var recipe = await this.recipesRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            recipe.Name = input.Name;
            recipe.Instructions = input.Instructions;
            recipe.CookingTIme = TimeSpan.FromMinutes(input.CookingTime);
            recipe.PreparationTime = TimeSpan.FromMinutes(input.PreparationTime);
            recipe.PortionsCount = input.PortionCount;
            recipe.CategoryId = input.CategoryId;

            await this.recipesRepository.SaveChangesAsync();
        }
    }
}
