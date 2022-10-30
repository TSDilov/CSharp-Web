namespace ReceptionApp.Web.ViewModels.SearchRecuipes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class SearchIndexViewModel
    {
        public IEnumerable<IngredientIdNameViewModel> Ingredients { get; set; }
    }
}
