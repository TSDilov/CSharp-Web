namespace ReceptionApp.Data.Models
{
    using System;

    using ReceptionApp.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }

        public string Extension { get; set; }

        public string RemoteImageUrl { get; set; }

        public string AddedByUserID { get; set; }

        public virtual ApplicationUser AddedByUser { get; set; }
    }
}
