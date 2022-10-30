namespace ReceptionApp.Web.ViewModels.SearchRecuipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ReceptionApp.Web.ViewModels.Recipes;

    public class ListViewModel
    {
        public IEnumerable<RecipeInListViewModel> Recipes { get; set; }
    }
}
