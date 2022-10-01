namespace ReceptionApp.Web.ViewModels.Recipes
{
    using System;
    using System.Collections.Generic;
    using ReceptionApp.Web.ViewModels;

    public class RecipesListViewModel : PagingViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
