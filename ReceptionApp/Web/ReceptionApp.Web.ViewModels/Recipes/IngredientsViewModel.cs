namespace ReceptionApp.Web.ViewModels.Recipes
{
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services.Mapping;

    public class IngredientsViewModel : IMapFrom<RecipeIngredient>
    {
        public string IngredientName { get; set; }

        public string Quantity { get; set; }
    }
}
