namespace ReceptionApp.Web.ViewModels.Recipes
{
    using AutoMapper;
    using ReceptionApp.Data.Models;
    using ReceptionApp.Services.Mapping;

    public class EditRecipeInputModel : BaseRecipeInputModel, IMapFrom<Recipe>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Recipe, EditRecipeInputModel>()
                .ForMember(x => x.CookingTime, opt =>
                    opt.MapFrom(x => (int)x.CookingTIme.TotalMinutes))
                .ForMember(x => x.PreparationTime, opt =>
                    opt.MapFrom(x => (int)x.PreparationTime.TotalMinutes));
        }
    }
}
