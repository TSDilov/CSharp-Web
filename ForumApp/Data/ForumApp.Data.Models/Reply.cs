namespace ForumApp.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using ForumApp.Data.Common.Models;

    public class Reply : BaseDeletableModel<string>
    {
        public Reply()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Likes = new HashSet<Like>();
        }

        [Required]
        public string Content { get; set; }

        public string TopicId { get; set; }

        public Topic Topic { get; set; }

        public string UserId { get; set; }

        public ICollection<Like> Likes { get; set; }
    }
}
