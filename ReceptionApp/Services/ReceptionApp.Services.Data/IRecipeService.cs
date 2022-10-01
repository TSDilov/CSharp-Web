namespace ReceptionApp.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ReceptionApp.Web.ViewModels.Recipes;

    public interface IRecipeService
    {
        Task CreateAsync(CreateRecipeModel input, string userId);

        IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12);

        int GetCount();
    }
}
