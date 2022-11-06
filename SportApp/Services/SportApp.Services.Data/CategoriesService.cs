namespace SportApp.Services.Data
{
    using SportApp.Data.Common.Repositories;
    using SportApp.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllCategoriesAsKeyValuePairs()
        {
            return this.categoryRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            })
                .OrderBy(x => x.Name)
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}
