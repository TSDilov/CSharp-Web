namespace ReceptionApp.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using ReceptionApp.Data.Common.Repositories;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services.Mapping;

    public class IngredientsService : IIngredientsService
    {
        private readonly IDeletableEntityRepository<Ingredient> ingredientRepository;

        public IngredientsService(IDeletableEntityRepository<Ingredient> ingredientRepository)
        {
            this.ingredientRepository = ingredientRepository;
        }

        public async Task<IEnumerable<T>> GetALlAsync<T>()
        {
            return await this.ingredientRepository.All()
                .OrderByDescending(x => x.Recipes.Count())
                .To<T>().ToListAsync();
        }
    }
}
