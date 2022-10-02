﻿namespace ReceptionApp.Data.Models
{
    using System;
    using System.Collections.Generic;

    using ReceptionApp.Data.Common.Models;

    public class Recipe : BaseDeletableModel<int>
    {
        public Recipe()
        {
            this.Ingredients = new HashSet<RecipeIngredient>();
            this.Images = new HashSet<Image>();
            this.Votes = new HashSet<Vote>();
        }

        public string Name { get; set; }

        public string Instructions { get; set; }

        public TimeSpan PreparationTime { get; set; }

        public TimeSpan CookingTIme { get; set; }

        public int PortionsCount { get; set; }

        public string OriginalUrl { get; set; }

        public string AddedByUserID { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<RecipeIngredient> Ingredients { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
