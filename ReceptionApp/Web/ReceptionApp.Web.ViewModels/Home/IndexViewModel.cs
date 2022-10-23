namespace ReceptionApp.Web.ViewModels.Home
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public int RecipesCount { get; set; }

        public int CategoriesCount { get; set; }

        public int IngredientsCount { get; set; }

        public int ImagesCount { get; set; }

        public IEnumerable<IndexPageRecipeViewModel> RandomRecipies { get; set; }
    }
}
