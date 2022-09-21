namespace ReceptionApp.Services.Data
{
    using System.Threading.Tasks;

    using ReceptionApp.Web.ViewModels.Recipes;

    public interface IRecipeService
    {
        Task CreateAsync(CreateRecipeModel input);
    }
}
