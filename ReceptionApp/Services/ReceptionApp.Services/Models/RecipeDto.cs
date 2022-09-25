namespace ReceptionApp.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class RecipeDto
    {
        public RecipeDto()
        {
            this.Ingredients = new Dictionary<string, string>();
        }

        public string CategoryName { get; set; }

        public string RecipeName { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTime { get; set; }

        public int PortionCount { get; set; }

        public string OriginalUrl { get; set; }

        public string Extension { get; set; }

        public IDictionary<string, string> Ingredients { get; set; }
    }
}
