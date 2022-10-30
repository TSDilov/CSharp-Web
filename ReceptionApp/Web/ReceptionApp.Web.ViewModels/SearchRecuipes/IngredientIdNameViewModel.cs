namespace ReceptionApp.Web.ViewModels.SearchRecuipes
{
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services.Mapping;

    public class IngredientIdNameViewModel : IMapFrom<Ingredient>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}